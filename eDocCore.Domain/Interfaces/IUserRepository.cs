using eDocCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eDocCore.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
