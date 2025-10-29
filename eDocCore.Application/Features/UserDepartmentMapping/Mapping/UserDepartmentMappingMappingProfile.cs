using AutoMapper;
using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.DTOs.Request;
using eDocCore.Domain.Entities;


namespace eDocCore.API.FeatureTemplate.eDocCore.Application.Features.UserDepartmentMapping.Mapping
{
    public class UserDepartmentMappingMappingProfile : Profile
    {
        public UserDepartmentMappingMappingProfile()
        {
            CreateMap<UserDepartmentMapping, DTOs.UserDepartmentMappingDto>();
            CreateMap<CreateUserDepartmentMappingRequest, UserDepartmentMapping>();
            CreateMap<UpdateUserDepartmentMappingRequest, UserDepartmentMapping>();
        }
    }
}