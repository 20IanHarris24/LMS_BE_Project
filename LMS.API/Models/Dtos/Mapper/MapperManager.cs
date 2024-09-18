using AutoMapper;
using LMS.API.Models.Entities;


namespace LMS.API.Models.Dtos.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager() {

            CreateMap<Activity, ActivityDto>();
            CreateMap<Module, ModuleDto>();
            CreateMap<ApplicationUser, UserForListDto>();
            CreateMap<Course, CourseDto>();
        
        }
    }
}
