using System.Threading;
using System.Threading.Tasks;
using eDocCore.Application.Common;
using eDocCore.Application.Features.Auth.DTOs.Request;
using eDocCore.Application.Features.Auth.DTOs.Response;
using eDocCore.Application.Features.Users.DTOs;

namespace eDocCore.Application.Features.Auth.Services
{
    public interface IAuthService
    {
        Task<UserDTO?> RegisterAsync(RegisterUserRequest request, CancellationToken ct = default);
    }
}
