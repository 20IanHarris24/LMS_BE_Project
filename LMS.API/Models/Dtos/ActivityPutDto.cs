using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class ActivityPutDto
    {
        //[Required] public Guid Id { get; set; }
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public Guid TypeId { get; init; }
        [Required] public DateTime? Start { get; init; }
        [Required] public DateTime? End { get; init; }
        [Required] public Guid ModuleId { get; init; }
    }
}
