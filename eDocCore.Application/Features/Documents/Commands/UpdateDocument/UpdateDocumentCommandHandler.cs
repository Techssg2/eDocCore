using AutoMapper;
using eDocCore.Domain.Entities;
using eDocCore.Domain.Exceptions;
using eDocCore.Domain.Interfaces;
using MediatR;

namespace eDocCore.Application.Features.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;

        public UpdateDocumentCommandHandler(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id</returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentToUpdate = await _documentRepository.GetByIdAsync(request.Id);

            if (documentToUpdate == null)
            {
                throw new NotFoundException(nameof(Document), request.Id);
            }

            _mapper.Map(request, documentToUpdate);
            await _documentRepository.UpdateAsync(documentToUpdate);
        }
    }
}
