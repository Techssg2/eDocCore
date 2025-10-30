using eDocCore.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Users.Services
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserByLoginName(string loginName, CancellationToken ct = default);
    }
}
