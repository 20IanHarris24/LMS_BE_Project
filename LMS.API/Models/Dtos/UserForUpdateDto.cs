using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public record UserForUpdateDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
        public string? Role { get; set; }

        public string? CourseID { get; set; }
    }
}
