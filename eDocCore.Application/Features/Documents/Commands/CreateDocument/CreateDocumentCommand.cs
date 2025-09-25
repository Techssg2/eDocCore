using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Documents.Commands.CreateDocument
{
    /// <summary>
    /// Command để tạo một document mới.
    /// </summary>
    public class CreateDocumentCommand : IRequest<Guid>
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Author { get; set; }
    }
}
