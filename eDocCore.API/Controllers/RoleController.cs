using eDocCore.Application.Features.Roles.DTOs;
using eDocCore.Application.Features.Roles.Services;
using eDocCore.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace eDocCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<RoleDto>>> GetPaged([FromQuery] GetRolesRequest request, CancellationToken ct)
        {
            var result = await _roleService.GetPagedInternalAsync(request, ct);
            return Ok(result);
        }

        // Hỗ trợ POST để nhận filter phức tạp qua body
        [HttpPost("paged")]
        public async Task<ActionResult<PagedResult<RoleDto>>> GetPagedPost([FromBody] GetRolesRequest request, CancellationToken ct)
        {
            var result = await _roleService.GetPagedInternalAsync(request, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var created = await _roleService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleRequest request)
        {
            if (id != request.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updated = await _roleService.UpdateAsync(request);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _roleService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
