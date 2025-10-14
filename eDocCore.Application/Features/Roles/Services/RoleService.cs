using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.Roles.DTOs;
using eDocCore.Domain.Entities;
using eDocCore.Domain.Interfaces.Extend;
using System.Linq;
using System.Linq.Expressions;

namespace eDocCore.Application.Features.Roles.Services
{
    /// <summary>
    /// Service đơn giản cho Role (không dùng CQRS)
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleDto?>(role);
        }

        public async Task<RoleDto> CreateAsync(CreateRoleRequest request)
        {
            var name = request.Name.Trim();
            if (await _roleRepository.ExistsByNameAsync(name))
                throw new InvalidOperationException("Role name already exists");

            var role = _mapper.Map<Role>(request);
            role.Name = name;
            role = await _roleRepository.AddAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UpdateAsync(UpdateRoleRequest request)
        {
            var existing = await _roleRepository.GetByIdAsync(request.Id);
            if (existing == null) return false;

            var name = request.Name.Trim();
            if (!string.Equals(existing.Name, name, StringComparison.OrdinalIgnoreCase)
                && await _roleRepository.ExistsByNameAsync(name))
            {
                throw new InvalidOperationException("Role name already exists");
            }

            _mapper.Map(request, existing);
            existing.Name = name;
            await _roleRepository.UpdateAsync(existing);
            return true;
        }

        public Task<bool> DeleteAsync(Guid id)
            => _roleRepository.DeleteAsync(id);

        public async Task<PagedResult<RoleDto>> GetPagedInternalAsync(GetRolesRequest request, System.Threading.CancellationToken ct = default)
        {
            // filter theo keyword & IsActive
            Expression<Func<Role, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(request.Keyword) || request.IsActive.HasValue)
            {
                var keyword = request.Keyword;
                filter = r =>
                    (string.IsNullOrWhiteSpace(keyword) || r.Name.Contains(keyword)) &&
                    (!request.IsActive.HasValue || r.IsActive == request.IsActive.Value);
            }

            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = q => q
                .OrderByDescending(x => x.Created)
                .ThenBy(x => x.Id); // deterministic

            // Project trực tiếp sang RoleDto để giảm IO và bỏ AutoMapper mapping ở client
            Expression<Func<Role, RoleDto>> selector = r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                IsActive = r.IsActive,
                Created = r.Created,
                Modified = r.Modified
            };

            var (items, total) = await _roleRepository.GetPagedProjectedAsync(
                request.Page,
                request.PageSize,
                filter,
                orderBy,
                selector,
                asNoTracking: true,
                ct: ct);

            return new PagedResult<RoleDto>
            {
                Items = items,
                TotalCount = total,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
