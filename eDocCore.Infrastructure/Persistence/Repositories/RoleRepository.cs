using eDocCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using eDocCore.Domain.Interfaces.Extend;

namespace eDocCore.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Set<Role>()
                .AnyAsync(r => r.Name.ToLower() == name.ToLower());
        }
    }
}
