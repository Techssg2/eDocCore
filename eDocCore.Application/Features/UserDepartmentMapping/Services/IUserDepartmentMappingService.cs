using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.DTOs;
using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.DTOs.Request;
using eDocCore.Application.Common.Models;

namespace eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.Services
{
    public interface IUserDepartmentMappingService
    {
        Task<IReadOnlyList<UserDepartmentMappingDto>> GetAllAsync();
        Task<UserDepartmentMappingDto?> GetByIdAsync(Guid id);
        Task<UserDepartmentMappingDto> CreateAsync(CreateUserDepartmentMappingRequest request);
        Task<bool> UpdateAsync(UpdateUserDepartmentMappingRequest request);
        Task<bool> DeleteAsync(Guid id);

        // Thêm API phân trang + filter
        Task<PagedResult<UserDepartmentMappingDto>> GetPagedInternalAsync(GetUserDepartmentMappingRequest request, System.Threading.CancellationToken ct = default);
    }
}