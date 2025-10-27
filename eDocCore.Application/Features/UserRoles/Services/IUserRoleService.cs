using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.UserRole.DTOs.Request;

namespace eDocCore.Application.Features.UserRole.Services
{
    public interface IUserRoleService
    {
        Task<IReadOnlyList<DTOs.UserRoleDto>> GetAllAsync();
        Task<DTOs.UserRoleDto?> GetByIdAsync(Guid id);
        Task<DTOs.UserRoleDto> CreateAsync(CreateUserRoleRequest request);
        Task<bool> UpdateAsync(UpdateUserRoleRequest request);
        Task<bool> DeleteAsync(Guid id);

        // Thêm API phân trang + filter
        Task<PagedResult<DTOs.UserRoleDto>> GetPagedInternalAsync(GetUserRoleRequest request, System.Threading.CancellationToken ct = default);
    }
}