using LMS.API.Models.Dtos;
using LMS.API.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/token")]

    [ApiController]

    public class TokenController(IAuthService authenticationService) : ControllerBase

    {

        [HttpPost("refresh")]

        public async Task<ActionResult<TokenDto>> RefreshToken(TokenDto token)

        {

            TokenDto tokenDto = await authenticationService.RefreshTokenAsync(token);

            return Ok(tokenDto);

        }

    }

}
