using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public class ActivityTypeDto
    {
        [Required] public string? Name { get; set; }
    }
}
