using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using eDocCore.Application.Roles.Commands;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Roles.Handlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var role = await _roleRepository.GetByIdAsync(request.Id);
                if (role == null) return false;
                role.Name = request.Name;
                await _roleRepository.UpdateAsync(role);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }
}
