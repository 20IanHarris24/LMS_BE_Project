using AutoMapper;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForListDto>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync(); 
            var userDtos = new List<UserForListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); 
                var userDto = _mapper.Map<UserForListDto>(user);
                userDto.Role = roles.FirstOrDefault();
                userDtos.Add(userDto);
            }

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserForListDto>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<UserForListDto>(user);
            userDto.Role = roles.FirstOrDefault();

            return Ok(userDto);
        }
    }
}
