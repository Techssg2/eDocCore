using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.__FeatureName__s.DTOs;
using eDocCore.Application.Features.__FeatureName__s.DTOs.Request;
using eDocCore.Application.Features.Auth.DTOs;
using eDocCore.Application.Features.Auth.DTOs.Request;

namespace eDocCore.Application.Features.__FeatureName__s.Services
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