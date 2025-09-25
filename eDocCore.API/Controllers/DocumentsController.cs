using eDocCore.Application.DTOs;
using eDocCore.Application.Features.Documents.Commands.CreateDocument;
using eDocCore.Application.Features.Documents.Commands.DeleteDocument;
using eDocCore.Application.Features.Documents.Commands.UpdateDocument;
using eDocCore.Application.Features.Documents.Queries.GetAllDocuments;
using eDocCore.Application.Features.Documents.Queries.GetDocumentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<DocumentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _mediator.Send(new GetAllDocumentsQuery());
            return Ok(documents);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var document = await _mediator.Send(new GetDocumentByIdQuery { Id = id });
            return Ok(document);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateDocumentCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateDocumentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteDocumentCommand { Id = id });
            return NoContent();
        }


    }
}
