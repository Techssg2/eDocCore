using System;
using MediatR;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Features.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
    }
}