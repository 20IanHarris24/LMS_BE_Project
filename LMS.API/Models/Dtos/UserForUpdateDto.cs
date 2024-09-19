using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public record UserForUpdateDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? CourseID { get; set; }
    }
}
