using eDocCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Domain.Interfaces
{
    /// <summary>
    /// Interface dành riêng cho Document repository, kế thừa từ IGenericRepository.
    /// Có thể thêm các phương thức truy vấn đặc thù cho Document ở đây.
    /// </summary>
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        // Ví dụ: Task<Document> GetDocumentByTitleAsync(string title);
    }
}
