using AutoMapper;
using LMS.API.Models.Entities;

namespace LMS.API.Models.Dtos.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<ApplicationUser, UserForListDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<ApplicationUser, UserForRegistrationDto>().ReverseMap();
            CreateMap<ApplicationUser, UserForUpdateDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseForUpdateDto>().ReverseMap();
        }
    }
}
