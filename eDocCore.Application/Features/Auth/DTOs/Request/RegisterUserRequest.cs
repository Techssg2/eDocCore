using System.ComponentModel.DataAnnotations;

namespace eDocCore.Application.Features.Auth.DTOs.Request
{
    public class RegisterUserRequest
    {
        [Required]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
