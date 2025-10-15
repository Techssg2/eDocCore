using eDocCore.Application.Features.Auth.DTOs.Request;
using FluentValidation;

namespace eDocCore.Application.Features.Auth.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.LoginName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
