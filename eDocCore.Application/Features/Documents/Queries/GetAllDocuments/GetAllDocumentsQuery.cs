using eDocCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Documents.Queries.GetAllDocuments
{
    public class GetAllDocumentsQuery : IRequest<IReadOnlyList<DocumentDto>>
    {
    }
}
