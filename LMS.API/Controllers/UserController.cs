using AutoMapper;
using Humanizer;
using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Teacher,Student")]
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
        [Authorize(Roles = "Teacher")]
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

        [HttpGet("courses/{id}")]
        public async Task<ActionResult<UserForListDto>> GetAllUsersByCourseId(string id)
        {
            var users = await _userManager.Users.Where(u => u.CourseId.ToString() == id).ToListAsync();
            if (users == null)
            {
                return NotFound();
            }

            var userDtos = new List<UserForListDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserForListDto>(user);
                userDto.Role = roles.FirstOrDefault();
                userDtos.Add(userDto);
            }
            //var roles = await _userManager.GetRolesAsync(users);
            return Ok(userDtos);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserForListDto>> UpdateUser(string id, UserForUpdateDto userDto)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            if (userDto.Role!.ToLower() == "student" && string.IsNullOrEmpty(userDto.CourseID))
            {
                return BadRequest("Student must have a course assigned.");
            }
            if (!string.IsNullOrEmpty(userDto.Role))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);  

                if (currentRoles.Any())
                {
                    var resultRemove = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!resultRemove.Succeeded)
                    {
                        return BadRequest("Failed to remove current roles.");
                    }
                }

                var resultAdd = await _userManager.AddToRoleAsync(user, userDto.Role);
                if (!resultAdd.Succeeded)
                {
                    return BadRequest("Failed to add the new role.");
                }
            }
            _mapper.Map(userDto,user);

            await _context.SaveChangesAsync();

            return Ok(userDto);
        }
        [Authorize(Roles = "Teacher")]
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

            if (!string.IsNullOrEmpty(userDto.Role))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);  

                if (currentRoles.Any())
                {
                    var resultRemove = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!resultRemove.Succeeded)
                    {
                        return BadRequest("Failed to remove current roles.");
                    }
                }

                var resultAdd = await _userManager.AddToRoleAsync(user, userDto.Role);
                if (!resultAdd.Succeeded)
                {
                    return BadRequest("Failed to add the new role.");
                }
            }

            if (!string.IsNullOrEmpty(userDto.CourseID))
            {
                var courseExists = await _context.Courses.AnyAsync(c => c.Id.ToString() == userDto.CourseID);
                if (!courseExists)
                {
                    return BadRequest("The specified course does not exist.");
                }
                user.CourseId = Guid.Parse(userDto.CourseID);  
            }
            _mapper.Map(userDto, user);

            await _context.SaveChangesAsync(); 

            var updatedUserDto = _mapper.Map<UserForListDto>(user);

            return Ok(updatedUserDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }



    }
}
