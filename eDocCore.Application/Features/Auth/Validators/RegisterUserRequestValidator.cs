using eDocCore.Application.Features.Auth.DTOs.Request;
using FluentValidation;

namespace eDocCore.Application.Features.Auth.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.LoginName)
                .NotEmpty().WithMessage("LoginName is required")
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid");
        }
    }
}
