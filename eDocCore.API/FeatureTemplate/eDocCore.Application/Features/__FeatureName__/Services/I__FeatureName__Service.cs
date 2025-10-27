using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.__FeatureName__.DTOs.Request;

namespace eDocCore.Application.Features.__FeatureName__.Services
{
    public interface I__FeatureName__Service
    {
        Task<IReadOnlyList<DTOs.__FeatureName__Dto>> GetAllAsync();
        Task<DTOs.__FeatureName__Dto?> GetByIdAsync(Guid id);
        Task<DTOs.__FeatureName__Dto> CreateAsync(Create__FeatureName__Request request);
        Task<bool> UpdateAsync(Update__FeatureName__Request request);
        Task<bool> DeleteAsync(Guid id);

        // Thêm API phân trang + filter
        Task<PagedResult<DTOs.__FeatureName__Dto>> GetPagedInternalAsync(Get__FeatureName__Request request, System.Threading.CancellationToken ct = default);
    }
}