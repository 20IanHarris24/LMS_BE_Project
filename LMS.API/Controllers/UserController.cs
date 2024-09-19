using AutoMapper;
using Humanizer;
using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace LMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, DatabaseContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
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

        [HttpPut("{id}")]
        public async Task<ActionResult<UserForListDto>> UpdateUser(string id, UserForUpdateDto userDto)
        {

            var user  = await _userManager.FindByIdAsync(id);
            
            if (user == null) 
            {
                return NotFound();
            }
            _mapper.Map(userDto,user);

            await _context.SaveChangesAsync();

            return Ok(userDto);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<UserForListDto>> PatchUser(string id, [FromBody] JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("The patch document cannot be null.");
            }

            var user = await _userManager.FindByIdAsync(id); 
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userDto = _mapper.Map<UserForUpdateDto>(user);

            patchDocument.ApplyTo(userDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(userDto.CourseID))
            {
                var courseExists = await _context.Courses.AnyAsync(c => c.Id.ToString() == userDto.CourseID);
                if (!courseExists)
                {
                    return BadRequest("The specified course does not exist.");
                }
                user.CourseID = userDto.CourseID;  
            }
            _mapper.Map(userDto, user);

            await _context.SaveChangesAsync(); 

            var updatedUserDto = _mapper.Map<UserForListDto>(user);

            return Ok(updatedUserDto);
        }





    }
}
