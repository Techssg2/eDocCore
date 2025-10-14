using System;

namespace eDocCore.Application.Features.Roles.DTOs
{
    public class UpdateRoleRequest
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public bool IsActive { get; init; } = true;
    }
}
