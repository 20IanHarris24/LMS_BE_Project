using LMS.API.Models.Entities;
using Microsoft.Build.Framework;

namespace LMS.API.Models.Dtos
{
    public record ActivityDto
    {
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public Guid TypeId { get; init; }
        [Required] public DateTime? Start {  get; init; }
        [Required] public DateTime? End { get; init; }
        [Required] public Guid ModuleId { get; init; }
    }
}
