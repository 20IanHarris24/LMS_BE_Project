using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos
{
    public record UserForListDto
    {
        [Required]
        public string? UserName { get; init; }

        [Required]
        public string? Email { get; init; }
    }
}
