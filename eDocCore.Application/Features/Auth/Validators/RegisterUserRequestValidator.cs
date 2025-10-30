using eDocCore.Application.Features.Auth.DTOs.Request;
using eDocCore.Application.Features.Auth.Services;
using eDocCore.Application.Features.Roles.Services;
using FluentValidation;

namespace eDocCore.Application.Features.Auth.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;

        public RegisterUserRequestValidator(IAuthService authService, IRoleService roleService)
        {
            _authService = authService;
            _roleService = roleService;

            RuleFor(x => x.LoginName)
                .NotEmpty().WithMessage("Login name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x)
            .Must((loginName, ct) => ExistRoleMember("Member"))
                .WithMessage("Role 'Member' does not exist.")
                .WithName("OtherError");
        }

        private bool ExistRoleMember(string roleName)
        {
            var roles = _roleService.GetRoleByNameAsync(roleName).Result;
            return roles != null;
        }
    }
}
