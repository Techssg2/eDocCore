using MediatR;
using eDocCore.Application.Users.Commands;
using eDocCore.Domain.Interfaces;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null) return false;
            user.LoginName = request.LoginName;
            user.FullName = request.FullName;
            user.Gender = request.Gender;
            user.Email = request.Email;
            user.IsActive = request.IsActive;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
