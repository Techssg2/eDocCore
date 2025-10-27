using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.__FeatureName__.DTOs;
using eDocCore.Application.Features.__FeatureName__.Services;
using eDocCore.Application.Features.Roles.DTOs;
using eDocCore.Application.Features.Roles.Services;
using Microsoft.AspNetCore.Mvc;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class __FeatureName__Controller : ControllerBase
    {
        private readonly I__FeatureName__Service ___FeatureName__Service;
        public __FeatureName__Controller(I__FeatureName__Service __FeatureName__Service)
        {
            ___FeatureName__Service = __FeatureName__Service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<IReadOnlyList<__FeatureName__Dto>>>> GetAll()
        {
            var roles = await ___FeatureName__Service.GetAllAsync();
            return Ok(ApiResult<IReadOnlyList<__FeatureName__Dto>>.Ok(roles, traceId: HttpContext.TraceIdentifier));
        }
    }
}