using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using LMS.API.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers;


[Route("api/authentication")]
[ApiController]
public class AutenticationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly DatabaseContext _context;

    public AutenticationController(IServiceManager serviceManager, DatabaseContext context)
    {
        _serviceManager = serviceManager;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        if (userForRegistration.Role == "student")
        {
            if (userForRegistration.AssignedCourse == null)
            {
                return BadRequest("AssignedCourse is required for students.");
            }
            var courseExists = await _context.Courses.AnyAsync(c => c.Id.ToString() == userForRegistration.AssignedCourse);

            if (!courseExists)
            {
                return BadRequest("must contain valid course id");
            }
        }

        var result = await _serviceManager.AuthService.RegisterUserAsync(userForRegistration);

        return result.Succeeded
            ? StatusCode(StatusCodes.Status201Created)
            : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(UserForAuthenticationDto user)
    {
        if (!await _serviceManager.AuthService.ValidateUserAsync(user))
            return Unauthorized();

        TokenDto tokenDto = await _serviceManager.AuthService.CreateTokenAsync(expireTime: true);
        return Ok(tokenDto);
    }
}

