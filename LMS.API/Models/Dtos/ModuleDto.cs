using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class ModuleDto : ModuleManipulationDto
    {
        public IEnumerable<ActivityListDto>? Activities { get; init; }
        [Required] public Guid Id { get; set; }
    }
}
