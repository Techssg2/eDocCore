using eDocCore.Domain.Entities;
using eDocCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eDocCore.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }
        // Thêm các method ??c thù cho Role n?u c?n
    }
}
