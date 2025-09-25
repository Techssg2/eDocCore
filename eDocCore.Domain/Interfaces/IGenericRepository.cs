using eDocCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Domain.Interfaces
{
    /// <summary>
    /// Interface chung cho Repository Pattern, định nghĩa các thao tác CRUD cơ bản.
    /// </summary>
    /// <typeparam name="T">Một lớp kế thừa từ BaseEntity.</typeparam>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
