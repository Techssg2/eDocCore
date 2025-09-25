using MediatR;
using eDocCore.Application.Users.Commands;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null) return false;
            await _userRepository.DeleteAsync(user);
            return true;
        }
    }
}
