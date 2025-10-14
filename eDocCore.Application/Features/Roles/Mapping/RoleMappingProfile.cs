using AutoMapper;
using eDocCore.Domain.Entities;

namespace eDocCore.Application.Features.Roles.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, DTOs.RoleDto>();
            CreateMap<DTOs.CreateRoleRequest, Role>();
            CreateMap<DTOs.UpdateRoleRequest, Role>();
        }
    }
}
