using System;
using MediatR;

namespace eDocCore.Application.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}