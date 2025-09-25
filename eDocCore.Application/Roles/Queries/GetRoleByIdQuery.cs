using System;
using MediatR;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<Role?>
    {
        public Guid Id { get; set; }
    }
}