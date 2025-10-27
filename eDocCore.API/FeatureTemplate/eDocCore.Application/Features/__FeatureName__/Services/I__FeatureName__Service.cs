using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.__FeatureName__.DTOs;
using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.__FeatureName__.DTOs.Request;
using eDocCore.Application.Common.Models;

namespace eDocCore.API.FeatureTemplate.eDocCore.Application.Features.__FeatureName__.Services
{
    public interface I__FeatureName__Service
    {
        Task<IReadOnlyList<__FeatureName__Dto>> GetAllAsync();
        Task<__FeatureName__Dto?> GetByIdAsync(Guid id);
        Task<__FeatureName__Dto> CreateAsync(Create__FeatureName__Request request);
        Task<bool> UpdateAsync(Update__FeatureName__Request request);
        Task<bool> DeleteAsync(Guid id);

        // Thêm API phân trang + filter
        Task<PagedResult<__FeatureName__Dto>> GetPagedInternalAsync(Get__FeatureName__Request request, System.Threading.CancellationToken ct = default);
    }
}