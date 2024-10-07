using LMS.API.Data;
using LMS.API.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Service.Contracts;

public interface IAuthService
{
    Task<TokenDto> CreateTokenAsync(bool expireTime);
    Task<TokenDto> RefreshTokenAsync(TokenDto token);
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration);
    Task<bool> ValidateUserAsync(UserForAuthenticationDto user);

    Task SeedUsersAsync(DatabaseContext db);
    
}
