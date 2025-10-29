using AutoMapper;
using eDocCore.API.FeatureTemplate.eDocCore.Application.Features.__FeatureName__.DTOs.Request;
using eDocCore.Domain.Entities;


namespace eDocCore.API.FeatureTemplate.eDocCore.Application.Features.__FeatureName__.Mapping
{
    public class __FeatureName__MappingProfile : Profile
    {
        public __FeatureName__MappingProfile()
        {
            CreateMap<__ModelName__, DTOs.__FeatureName__Dto>();
            CreateMap<Create__FeatureName__Request, __ModelName__>();
            CreateMap<Update__FeatureName__Request, __ModelName__>();
        }
    }
}