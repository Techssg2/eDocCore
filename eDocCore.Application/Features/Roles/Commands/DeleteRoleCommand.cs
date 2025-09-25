using System;
using MediatR;

namespace eDocCore.Application.Features.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}