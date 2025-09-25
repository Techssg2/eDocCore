using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Entities;
using eDocCore.Application.Features.Roles.Commands;

namespace eDocCore.Application.Features.Roles.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var role = new Role { Name = request.Name };
                var createdRole = await _roleRepository.AddAsync(role);
                await _unitOfWork.CommitAsync();
                return createdRole.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
