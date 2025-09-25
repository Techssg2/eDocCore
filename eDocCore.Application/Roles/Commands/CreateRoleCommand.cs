using System;
using MediatR;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
    }
}