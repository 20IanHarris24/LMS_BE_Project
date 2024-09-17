using Microsoft.AspNetCore.Identity;

namespace LMS.API.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
