using System.Threading;
using System.Threading.Tasks;
using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.Auth.DTOs.Request;
using eDocCore.Application.Features.Auth.DTOs.Response;
using eDocCore.Application.Features.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResult<string>>> Register([FromBody] RegisterUserRequest request, CancellationToken ct)
        {
            var ok = await _authService.RegisterAsync(request, ct);
            if (!ok) return BadRequest(ApiResult<string>.Fail("Register failed", traceId: HttpContext.TraceIdentifier));
            return Ok(ApiResult<string>.Ok("Registered", traceId: HttpContext.TraceIdentifier));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResult<LoginResponse>>> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request, ct);
            if (result == null) return Unauthorized(ApiResult<LoginResponse>.Fail("Invalid credentials", traceId: HttpContext.TraceIdentifier));
            return Ok(ApiResult<LoginResponse>.Ok(result, traceId: HttpContext.TraceIdentifier));
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResult<string>>> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            var ok = await _authService.ChangePasswordAsync(userId!, request, ct);
            return Ok(ApiResult<string>.Ok("Password changed", traceId: HttpContext.TraceIdentifier));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<ApiResult<CurrentUserResponse>>> Me(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(ApiResult<CurrentUserResponse>.Fail("Unauthorized", traceId: HttpContext.TraceIdentifier));

            var user = await _authService.GetCurrentUserAsync(userId, ct);
            if (user == null) return NotFound(ApiResult<CurrentUserResponse>.Fail("Not found", traceId: HttpContext.TraceIdentifier));
            return Ok(ApiResult<CurrentUserResponse>.Ok(user, traceId: HttpContext.TraceIdentifier));
        }
    }
}
