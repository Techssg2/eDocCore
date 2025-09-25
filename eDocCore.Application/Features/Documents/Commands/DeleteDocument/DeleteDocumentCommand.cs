using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Documents.Commands.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
