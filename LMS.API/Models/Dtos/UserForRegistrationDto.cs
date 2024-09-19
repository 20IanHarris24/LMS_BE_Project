using LMS.API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.Dtos;

public record UserForRegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    public string? UserName { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; init; }
    [Required(ErrorMessage = "Role is required")]
    public string? Role { get; init; }

    public string? AssignedCourse { get; set; }
}
