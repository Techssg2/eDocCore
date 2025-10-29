using eDocCore.Application.Common.Exceptions;
using eDocCore.Application.Common.Models;
using eDocCore.Application.Features.Auth.DTOs.Request;
using eDocCore.Application.Features.Auth.DTOs.Response;
using eDocCore.Application.Features.Auth.Services;
using eDocCore.Application.Features.Auth.Validators; // Added missing namespace
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResult<string>>> Register([FromBody] RegisterUserRequest request, CancellationToken ct)
        {
            //1. Validate request using FluentValidation
            var validator = new RegisterUserRequestValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                // Trả về lỗi nếu validation thất bại
                return BadRequest(ApiResult<string>.Fail(
                    "Validation failed",
                    validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList(),
                    traceId: HttpContext.TraceIdentifier));
            }

            //2. Call service to handle business logic
            try
            {
                var result = await _authService.RegisterAsync(request, ct);
                if (!result.IsSuccess)
                {
                    return BadRequest(ApiResult<string>.Fail(
                        result.Message,
                        result.ErrorMessages,
                        traceId: HttpContext.TraceIdentifier));
                }

                return Ok(ApiResult<string>.Ok(
                    null,
                    result.Message,
                    traceId: HttpContext.TraceIdentifier));
            }
            catch (BusinessRuleException ex)
            {
                // Xử lý lỗi nghiệp vụ
                _logger.LogError($"Business rule error: {ex.Message}");
                return BadRequest(ApiResult<string>.Fail(
                    ex.Message,
                    traceId: HttpContext.TraceIdentifier));
            }
            catch (Exception ex)
            {
                // Xử lý lỗi không mong muốn
                _logger.LogError($"Unexpected error: {ex.Message}");
                return StatusCode(500, ApiResult<string>.Fail(
                    "An unexpected error occurred",
                    traceId: HttpContext.TraceIdentifier));
            }
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
