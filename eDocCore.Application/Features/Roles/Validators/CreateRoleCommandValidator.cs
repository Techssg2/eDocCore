using FluentValidation;
using eDocCore.Application.Features.Roles.Commands;
using eDocCore.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Roles.Validators
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator(IRoleRepository roleRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.")
                .MustAsync(async (name, cancellation) =>
                {
                    return !await roleRepository.ExistsByNameAsync(name);
                }).WithMessage("Role name already exists.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Role name must not contain special characters."); ;
        }
    }
}
