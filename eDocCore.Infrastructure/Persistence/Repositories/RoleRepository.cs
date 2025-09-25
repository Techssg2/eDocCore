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
        // Th�m c�c method ??c th� cho Role n?u c?n
    }
}
