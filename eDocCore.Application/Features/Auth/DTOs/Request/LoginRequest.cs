using System.ComponentModel.DataAnnotations;

namespace eDocCore.Application.Features.Auth.DTOs.Request
{
    public class LoginRequest
    {
        [Required]
        public string LoginName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
