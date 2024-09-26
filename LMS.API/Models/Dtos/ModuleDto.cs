using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class ModuleDto : ModuleManipulationDto
    {
        public IEnumerable<ActivityDto>? Activities { get; init; }
        [Required] public Guid Id { get; set; }
    }
}
