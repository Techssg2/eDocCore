using System;
using System.Collections.Generic;

namespace eDocCore.Domain.Entities;

public partial class User
{
    public string LoginName { get; set; } = null!;

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public bool? Gender { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
