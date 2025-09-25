using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Entities;
using eDocCore.Application.Features.Roles.Queries;

namespace eDocCore.Application.Features.Roles.Handlers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Role?>
    {
        private readonly IRoleRepository _roleRepository;
        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Role?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetByIdAsync(request.Id);
        }
    }
}
