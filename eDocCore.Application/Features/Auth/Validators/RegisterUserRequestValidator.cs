using FluentValidation;
using eDocCore.Application.Features.Auth.DTOs.Request;

namespace eDocCore.Application.Features.Auth.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
