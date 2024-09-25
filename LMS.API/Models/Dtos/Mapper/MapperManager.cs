using AutoMapper;
using LMS.API.Models.Entities;

namespace LMS.API.Models.Dtos.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Activity, ActivityPutDto>().ReverseMap();
            
            CreateMap<Activity, ActivityListDto>()
                .ForMember(dest => dest.ActivityType, opt => 
                                                      opt.MapFrom (src => $"{src.Type!.Name}"));


            CreateMap<ActivityType, ActivityTypeDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleManipulationDto>().ReverseMap();
            CreateMap<Module, ModuleForUpdateDto>().ReverseMap();
            CreateMap<ApplicationUser, UserForListDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserForRegistrationDto>().ReverseMap();
            CreateMap<ApplicationUser, UserForUpdateDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseForUpdateDto>().ReverseMap();
            CreateMap<ModuleForUpdateDto, Module>().ReverseMap();
        }
    }
}
