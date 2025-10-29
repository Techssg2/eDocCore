using AutoMapper;
using eDocCore.Application.Common.Interfaces;
using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.__FeatureName__s.DTOs;
using eDocCore.Application.Features.__FeatureName__s.DTOs.Request;
using eDocCore.Domain.Entities;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Interfaces.Extend;
using Microsoft.Extensions.Logging;

namespace eDocCore.Application.Features.__FeatureName__s.Services
{
    public class __FeatureName__Service : I__FeatureName__Service
    {

        private readonly I__FeatureName__Repository ___FeatureName__Repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<__FeatureName__Service> _logger;
        private readonly I__FeatureName__Validator _validator;
        private readonly ICurrentUser _currentUser;

        public __FeatureName__Service(I__FeatureName__Repository __FeatureName__Repository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<__FeatureName__Service> logger, I__FeatureName__Validator validator, ICurrentUser currentUser)
        {
            ___FeatureName__Repository = __FeatureName__Repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
            _currentUser = currentUser;
        }

        public async Task<IReadOnlyList<__FeatureName__Dto>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all __FeatureName__s by {UserId}", _currentUser.UserId);
            var __FeatureName__s = await ___FeatureName__Repository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<__FeatureName__Dto>>(__FeatureName__s);
        }

        public async Task<__FeatureName__Dto?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Fetching __FeatureName__ by id {__FeatureName__Id} by {UserId}", id, _currentUser.UserId);
            var __FeatureName__ = await ___FeatureName__Repository.GetByIdAsync(id);
            return _mapper.Map<__FeatureName__Dto?>(__FeatureName__);
        }

        public async Task<__FeatureName__Dto> CreateAsync(Create__FeatureName__Request request)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                
                var __FeatureName__ = _mapper.Map<__ModelName__>(request);
                __FeatureName__ = await ___FeatureName__Repository.AddAsync(__FeatureName__);

                await _unitOfWork.CommitAsync();
                return _mapper.Map<__FeatureName__Dto>(__FeatureName__);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating __FeatureName__ by {UserId}", _currentUser.UserId);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Update__FeatureName__Request request)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Updating __FeatureName__ {__FeatureName__Id} by {UserId}", request.Id, _currentUser.UserId);
                var existing = await ___FeatureName__Repository.GetByIdAsync(request.Id);
                _mapper.Map(request, existing);

                if (existing != null)
                {
                    await ___FeatureName__Repository.UpdateAsync(existing);
                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Updated __FeatureName__ {__FeatureName__Id} by {UserId}", request.Id, _currentUser.UserId);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating __FeatureName__ {__FeatureName__Id} by {UserId}", request.Id, _currentUser.UserId);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Deleting __FeatureName__ {__FeatureName__Id} by {UserId}", id, _currentUser.UserId);
                var deleted = await ___FeatureName__Repository.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning("__FeatureName__ {__FeatureName__Id} not found for delete by {UserId}", id, _currentUser.UserId);
                    await _unitOfWork.RollbackAsync();
                    return false;
                }

                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Deleted __FeatureName__ {__FeatureName__Id} by {UserId}", id, _currentUser.UserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting __FeatureName__ {__FeatureName__Id} by {UserId}", id, _currentUser.UserId);
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public Task<PagedResult<__FeatureName__Dto>> GetPagedInternalAsync(Get__FeatureName__Request request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /*public async Task<PagedResult<__FeatureName__Dto>> GetPagedInternalAsync(Get__FeatureName__Request request, System.Threading.CancellationToken ct = default)
        {
            // filter theo keyword & IsActive
            Expression<Func<__ModelName__, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var keyword = request.Keyword;
                //filter = r => (string.IsNullOrWhiteSpace(keyword) || r.Name.Contains(keyword));
            }

            Func<IQueryable<__ModelName__>, IOrderedQueryable<__ModelName__>> orderBy = q => q
                .OrderByDescending(x => x.)
                .ThenBy(x => x.Id); // deterministic

            // Project trực tiếp sang __FeatureName__Dto để giảm IO và bỏ AutoMapper mapping ở client
            Expression<Func<__ModelName__, __FeatureName__Dto>> selector = r => new __FeatureName__Dto
            {
                Id = r.Id,
                
            };

            var (items, total) = await ___FeatureName__Repository.GetPagedProjectedAsync(
                request.Page,
                request.PageSize,
                filter,
                orderBy,
                selector,
                asNoTracking: true,
                ct: ct);

            return new PagedResult<__FeatureName__Dto>
            {
                Items = items,
                TotalCount = total,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }*/
    }
}