using AutoMapper;
using eDocCore.Application.Features.Users.DTOs;
using eDocCore.Domain.Interfaces.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Application.Features.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO?> GetUserByLoginName(string loginName, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByLoginNameAsync(loginName);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }
    }
}
