using LMS.API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public record UserForListDto 
    {
        [Required]
        public string? UserName { get; init; }

        [Required]
        public string? Email { get; init; }

        public string? Role { get; set; }
        public string? CourseID { get; set; }

    }
}
