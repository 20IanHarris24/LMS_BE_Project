using AutoMapper;
using LMS.API.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace LMS.API.Models.Dtos.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager() {

            CreateMap<Activity, ActivityDto>();
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<ApplicationUser, UserForListDto>();
            CreateMap<ApplicationUser, UserForRegistrationDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<ApplicationUser, UserForUpdateDto>().ReverseMap();

            CreateMap<ApplicationUser, UserForListDto>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            //CreateMap<Course, CourseDto>().ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules)).ReverseMap();

        }
    }
}
