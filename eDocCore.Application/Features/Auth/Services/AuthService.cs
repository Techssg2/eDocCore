using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eDocCore.Application.Common.Exceptions;
using eDocCore.Application.Common.Interfaces;
using eDocCore.Application.Common.Security;
using eDocCore.Application.Features.Auth.DTOs.Request;
using eDocCore.Application.Features.Auth.DTOs.Response;
using eDocCore.Domain.Entities;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Interfaces.Extend;

namespace eDocCore.Application.Features.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenericRepository<eDocCore.Domain.Entities.UserRole> _userRole;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwt;

        public AuthService(IUserRepository userRepository, IGenericRepository<eDocCore.Domain.Entities.UserRole> userRole, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IJwtTokenService jwt)
        {
            _userRepository = userRepository;
            _userRole = userRole;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _jwt = jwt;
        }

        public async Task<bool> RegisterAsync(RegisterUserRequest request, CancellationToken ct = default)
        {
            // Logic nghiệp vụ: Kiểm tra sự tồn tại của người dùng
            await EnsureUserDoesNotExist(request.LoginName);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Tạo người dùng
                var user = CreateUser(request);
                await _userRepository.AddAsync(user);

                // Gán vai trò mặc định
                await AssignDefaultRole(user.Id);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task EnsureUserDoesNotExist(string loginName)
        {
            var exists = await _userRepository.GetByLoginNameAsync(loginName);
            if (exists != null) throw new ConflictException("Login name already exists");
        }

        private User CreateUser(RegisterUserRequest request)
        {
            return new User
            {
                LoginName = request.LoginName,
                Password = PasswordHasher.Hash(request.Password),
                FullName = request.FullName,
                Email = request.Email,
                IsActive = request.IsActive,
                Created = DateTimeOffset.UtcNow,
                Modified = DateTimeOffset.UtcNow
            };
        }

        private async Task AssignDefaultRole(Guid userId)
        {
            var roleDefault = await _roleRepository.FirstOrDefaultAsync(x => x.Name == "Member");
            if (roleDefault == null) throw new NotFoundAppException("Default role 'Member' not found");

            var userRole = new UserRole { UserId = userId, RoleId = roleDefault.Id };
            await _userRole.AddAsync(userRole);
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByLoginNameAsync(request.LoginName);
            if (user == null || string.IsNullOrEmpty(user.Password) || !PasswordHasher.Verify(user.Password, request.Password))
            {
                return null;
            }
            if (!user.IsActive)
            {
                throw new BusinessRuleException("User is inactive");
            }

            var roles = await _userRepository.GetRoleNamesAsync(user.Id);
            return _jwt.GenerateToken(user, roles);
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request, CancellationToken ct = default)
        {
            if (!Guid.TryParse(userId, out var id)) throw new ValidationAppException("Invalid user id");
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new NotFoundAppException("User not found");
            if (string.IsNullOrEmpty(user.Password) || !PasswordHasher.Verify(user.Password, request.CurrentPassword))
                throw new BusinessRuleException("Current password is incorrect");

            user.Password = PasswordHasher.Hash(request.NewPassword);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<CurrentUserResponse?> GetCurrentUserAsync(string userId, CancellationToken ct = default)
        {
            if (!Guid.TryParse(userId, out var id)) return null;
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;
            var roles = await _userRepository.GetRoleNamesAsync(user.Id);
            return new CurrentUserResponse
            {
                Id = user.Id,
                LoginName = user.LoginName,
                FullName = user.FullName,
                Email = user.Email,
                Roles = roles,
                IsActive = user.IsActive
            };
        }
    }
}
