using AutoMapper;
using LMS.API.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace LMS.API.Models.Dtos.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager() {

            CreateMap<Activity, ActivityDto>();
            CreateMap<Module, ModuleDto>();
            CreateMap<ApplicationUser, UserForListDto>();
            CreateMap<ApplicationUser, UserForRegistrationDto>().ReverseMap();
            CreateMap<Course, CourseDto>();
        
        }
    }
}
