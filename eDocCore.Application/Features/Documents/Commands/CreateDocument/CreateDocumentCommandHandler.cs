using AutoMapper;
using eDocCore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using eDocCore.Domain.Entities;
using Document = eDocCore.Domain.Entities.Document;

namespace eDocCore.Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Guid>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;

        public CreateDocumentCommandHandler(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _mapper.Map<Document>(request);
            var newDocument = await _documentRepository.AddAsync(document);
            return newDocument.Id;
        }
    }

}
