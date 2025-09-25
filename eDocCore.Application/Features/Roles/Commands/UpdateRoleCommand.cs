using System;
using MediatR;

namespace eDocCore.Application.Features.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}