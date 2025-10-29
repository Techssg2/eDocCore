using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.DTOs;
using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.Services;
using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.Roles.DTOs;
using eDocCore.Application.Features.Roles.Services;
using Microsoft.AspNetCore.Mvc;

namespace eDocCore.API.FeatureTemplate.eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDepartmentMappingController : ControllerBase
    {
        private readonly IUserDepartmentMappingService _UserDepartmentMappingService;
        public UserDepartmentMappingController(IUserDepartmentMappingService UserDepartmentMappingService)
        {
            _UserDepartmentMappingService = UserDepartmentMappingService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<IReadOnlyList<UserDepartmentMappingDto>>>> GetAll()
        {
            var roles = await _UserDepartmentMappingService.GetAllAsync();
            return Ok(ApiResult<IReadOnlyList<UserDepartmentMappingDto>>.Ok(roles, traceId: HttpContext.TraceIdentifier));
        }
    }
}