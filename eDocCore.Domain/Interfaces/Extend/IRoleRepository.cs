using eDocCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eDocCore.Domain.Interfaces.Extend
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
