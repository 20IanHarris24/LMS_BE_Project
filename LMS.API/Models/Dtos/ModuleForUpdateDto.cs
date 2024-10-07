using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class ModuleForUpdateDto : ModuleManipulationDto
    {
        [Required] public Guid CourseId { get; set; }
    }
}
