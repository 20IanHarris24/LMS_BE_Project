﻿using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public record CourseDto
    {
        [Required]public Guid Id { get; set; }
        [Required] public string? Name { get; init; }
        [Required] public string? Description { get; init; }
        [Required] public DateTime Start {  get; init; }
        public IEnumerable<ModuleDto>? Modules { get; init; }
    }
}
