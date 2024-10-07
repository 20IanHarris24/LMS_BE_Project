using AutoMapper;
using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using LMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ServiceManager _serviceManager;
        public CourseController(IMapper mapper, DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            //_serviceManager = serviceManager;

        }
        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            try
            {
                var courses = await _context.Courses.Include(c => c.Modules).ToListAsync();
                var courseDtos = _mapper.Map<List<CourseDto>>(courses);

                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = _mapper.Map<Course>(courseDto);
                _context.Set<Course>().Add(course);
                await _context.SaveChangesAsync();
                var result = _mapper.Map<CourseDto>(course);

                return CreatedAtAction(nameof(CreateCourse), new { id = result.id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Teacher,Student")]
        [HttpGet("{user_id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(string user_id)
        {
            var user = await _context.Users
                .Where(u => u.Id == user_id)
                .Select(u => new { u.CourseId })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!user.CourseId.HasValue)
            {
                return NotFound("User does not have an assigned course.");
            }

            var course = await _context.Courses
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == user.CourseId);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("getCourseById/{id}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(string id)
        {
            var course = await _context.Courses
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id.ToString() == id);

            if (course == null)
            {
                return NotFound("User not found.");
            }

            var courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _context.Set<Course>()
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(string id, CourseForUpdateDto courseDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id.ToString() == id);
            if (course == null)
            {
                return NotFound();
            }
            _mapper.Map(courseDto, course);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<CourseForUpdateDto>(course));

        }

        [HttpGet("usercourses")]
        public async Task<ActionResult<IEnumerable<UserCourseDto>>> GetUsersWithCourses()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                var courses = await _context.Courses.ToListAsync();

                var courseMap = courses.ToDictionary(course => course.Id, course => course.Name);

                var userCourseDtos = users.Select(user => new UserCourseDto
                {
                    UserName = user.UserName,
                    CourseName = user.CourseId.HasValue && courseMap.TryGetValue(user.CourseId.Value, out var courseName)
                        ? courseName
                        : "No Course Assigned"
                }).ToList();

                return Ok(userCourseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching users and courses.");
            }
        }





    }
}
