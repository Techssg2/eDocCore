using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.UserRole.DTOs;
using eDocCore.Application.Features.UserRole.Services;
using eDocCore.Application.Features.Roles.DTOs;
using eDocCore.Application.Features.Roles.Services;
using Microsoft.AspNetCore.Mvc;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _UserRoleService;
        public UserRoleController(IUserRoleService UserRoleService)
        {
            _UserRoleService = UserRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<IReadOnlyList<UserRoleDto>>>> GetAll()
        {
            var roles = await _UserRoleService.GetAllAsync();
            return Ok(ApiResult<IReadOnlyList<UserRoleDto>>.Ok(roles, traceId: HttpContext.TraceIdentifier));
        }
    }
}