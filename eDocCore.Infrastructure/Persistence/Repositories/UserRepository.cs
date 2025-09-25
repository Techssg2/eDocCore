using eDocCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eDocCore.Domain.Entities;

namespace eDocCore.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Guid> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.Created = DateTimeOffset.UtcNow;
            user.Modified = DateTimeOffset.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task UpdateAsync(User user)
        {
            user.Modified = DateTimeOffset.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
