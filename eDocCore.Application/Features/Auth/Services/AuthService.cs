using eDocCore.Application.Common;
using eDocCore.Application.Common.Exceptions;
using eDocCore.Application.Common.Interfaces;
using eDocCore.Application.Common.Security;
using eDocCore.Application.Features.Auth.DTOs.Request;
using eDocCore.Application.Features.Auth.DTOs.Response;
using eDocCore.Application.Features.UserRoles.Services;
using eDocCore.Domain.Entities;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Interfaces.Extend;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenericRepository<UserRole> _userRole;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IGenericRepository<eDocCore.Domain.Entities.UserRole> userRole, IRoleRepository roleRepository, IUnitOfWork unitOfWork, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _userRole = userRole;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(RegisterUserRequest request, CancellationToken ct = default)
        {
            try
            {
                // Tạo người dùng
                var user = new User
                {
                    LoginName = request.LoginName,
                    Password = PasswordHasher.Hash(request.Password),
                    FullName = request.FullName,
                    Email = request.Email,
                    IsActive = request.IsActive,
                }; ;
                await _userRepository.AddAsync(user);

                // Gán vai trò mặc định
                await AssignDefaultRole(user.Id);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        private async Task AssignDefaultRole(Guid userId)
        {
            var roleDefault = await _roleRepository.FirstOrDefaultAsync(x => x.Name == "Member");
            if (roleDefault == null) throw new BusinessRuleException("Default role 'Member' not found");

            var userRole = new UserRole { UserId = userId, RoleId = roleDefault.Id };
            await _userRole.AddAsync(userRole);
        }

        /*public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default)
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
        }*/
    }
}
