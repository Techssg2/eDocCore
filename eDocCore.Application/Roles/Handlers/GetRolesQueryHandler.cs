using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using eDocCore.Application.Roles.Queries;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Roles.Handlers
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<Role>>
    {
        private readonly IRoleRepository _roleRepository;
        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<List<Role>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return (await _roleRepository.GetAllAsync()).ToList();
        }
    }
}
