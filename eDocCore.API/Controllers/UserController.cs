using MediatR;
using Microsoft.AspNetCore.Mvc;
using eDocCore.Application.Users.Commands;
using eDocCore.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy danh sách User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }

        // POST: /User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUsers), new { id }, command);
        }

        // PUT: /User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: /User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
