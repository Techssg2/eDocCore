using MediatR;
using System;

namespace eDocCore.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
