using LMS.API.Models.Dtos;
using LMS.API.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public TokenController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenDto>> RefreshToken(TokenDto token)
        {
            if (token == null)
            {
                return BadRequest("Invalid client request: token cannot be null.");
            }

            try
            {
                TokenDto tokenDto = await _authenticationService.RefreshTokenAsync(token);
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
