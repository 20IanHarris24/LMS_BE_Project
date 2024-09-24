using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class ModuleManipulationDto
    {
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public DateTime? Start {  get; init; }
        [Required] public DateTime? End { get; init; }
    }
}
