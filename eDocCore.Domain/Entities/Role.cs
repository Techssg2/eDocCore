using System;
using System.Collections.Generic;

namespace eDocCore.Domain.Entities;

public partial class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
