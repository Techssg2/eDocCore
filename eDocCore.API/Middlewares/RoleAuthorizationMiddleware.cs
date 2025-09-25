using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eDocCore.API.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _allowedRoles;

        public RoleAuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            // L?y danh s�ch role t? c?u h�nh appsettings.json
            _allowedRoles = configuration.GetSection("AllowedRoles").Get<string[]>() ?? new string[0];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userRoles = context.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();

                if (!_allowedRoles.Any(role => userRoles.Contains(role)))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: You do not have the required role.");
                    return;
                }
            }
            await _next(context);
        }
    }
}
