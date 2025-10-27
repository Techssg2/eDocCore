using AutoMapper;
using eDocCore.Application.Features.UserRole.DTOs.Request;
using eDocCore.Application.Features.Roles.DTOs.Request;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Features.UserRole.Mapping
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<eDocCore.Domain.Entities.UserRole, DTOs.UserRoleDto>();
            CreateMap<CreateUserRoleRequest, eDocCore.Domain.Entities.UserRole>();
            CreateMap<UpdateUserRoleRequest, eDocCore.Domain.Entities.UserRole>();
        }
    }
}