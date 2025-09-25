using eDocCore.Domain.Entities;
using eDocCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
