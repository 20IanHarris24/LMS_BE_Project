using AutoMapper;
using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public UserController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForListDto>>> GetUsers() 
        {
            var users = await _context.Set<ApplicationUser>().ToListAsync();
            var userDtos = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserForListDto>> GetUser(Guid id) 
        {
            var user = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.Id == id.ToString());
            var userDto = _mapper.Map<UserForListDto>(user);
            return Ok(userDto);
        }
    }
}
