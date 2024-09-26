using AutoMapper;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using LMS.API.Service.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LMS.API.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;
    private readonly RoleManager<IdentityRole> roleManager;
    private ApplicationUser? user;
    private readonly IMapper mapper;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.roleManager = roleManager;
        this.mapper = mapper;

        SeedUsersAsync().GetAwaiter().GetResult();
    }


    public async Task<TokenDto> CreateTokenAsync(bool expireTime)
    {
        SigningCredentials signing = GetSigningCredentials();
        IEnumerable<Claim> claims = GetClaims();
        JwtSecurityToken tokenOptions = GenerateTokenOptions(signing, claims);

        ArgumentNullException.ThrowIfNull(user, nameof(user));
        user.RefreshToken = GenerateRefreshToken();

        if (expireTime)
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(2);

        var res = await userManager.UpdateAsync(user); //ToDo validate res!
        string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(accessToken, user.RefreshToken);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signing, IEnumerable<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"], // Ensure this is set
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
            signingCredentials: signing
        );

        return tokenOptions;
    }

    private IEnumerable<Claim> GetClaims()
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            //Add more if needed
        };
        
        var roles = userManager.GetRolesAsync(user).Result;

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        string? secretKey = configuration["secretkey"];
        ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));

        byte[] key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

    }
        static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Teacher")) { await roleManager.CreateAsync(new IdentityRole("Teacher")); }
            if (!await roleManager.RoleExistsAsync("Student")) { await roleManager.CreateAsync(new IdentityRole("Student")); }
        }




    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration)
    {
        ArgumentNullException.ThrowIfNull(userForRegistration, nameof(userForRegistration));

        //var user = new ApplicationUser
        //{
        //    UserName = userForRegistration.UserName,
        //    Email = userForRegistration.Email,
        //};
        var user = mapper.Map<ApplicationUser>(userForRegistration);

        await SeedRoles(roleManager);
        var result = await userManager.CreateAsync(user, userForRegistration.Password!);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, userForRegistration.Role!); 
        }

        return result;
    }

    public async Task<bool> ValidateUserAsync(UserForAuthenticationDto userDto)
    {
        ArgumentNullException.ThrowIfNull(userDto, nameof(userDto));

        user = await userManager.FindByNameAsync(userDto.UserName!);

        return user != null && await userManager.CheckPasswordAsync(user, userDto.Password!);
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto token)
    {
        ClaimsPrincipal principal = GetPrincipalFromExpiredToken(token.AccessToken);

        ApplicationUser? user = await userManager.FindByNameAsync(principal.Identity?.Name!);
        if (user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpireTime <= DateTime.Now)

            //ToDo: Handle with middleware and custom exception class
            throw new ArgumentException("The TokenDto has som invalid values");

        this.user = user;

        return await CreateTokenAsync(expireTime: false);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        IConfigurationSection jwtSettings = configuration.GetSection("JwtSettings");

        string? secretKey = configuration["secretkey"];
        ArgumentNullException.ThrowIfNull(nameof(secretKey));

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };

        JwtSecurityTokenHandler tokenHandler = new();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
    public async Task SeedUsersAsync()
    {
        await SeedRoles(roleManager);

        var users = new List<UserForRegistrationDto>
        {
            new UserForRegistrationDto
            {
                UserName = "Teacher",
                Password = "Password123!",
                Email = "teacher@example.com",
                Role = "Teacher"
                
            },
            new UserForRegistrationDto
            {
                UserName = "Student",
                Password = "Password123!",
                Email = "student1@example.com",
                Role = "Student",
                CourseID = "a767cdee-e833-427a-9349-3ee71cca8a39"
            },
            new UserForRegistrationDto
            {
                UserName = "Student2",
                Password = "Password123!",
                Email = "student2@example.com",
                Role = "Student",
                CourseID = "6f01e571-41f0-4789-8059-422ae07d736e"
            }
        };

        foreach (var userDto in users)
        {
            var result = await RegisterUserAsync(userDto);
            if (!result.Succeeded)
            {
                Console.WriteLine($"Failed to create user {userDto.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }

    public static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Key"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" // Set role claim type
            };
        });
    }

}

