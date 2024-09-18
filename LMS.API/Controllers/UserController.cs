using LMS.API.Data;
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
        public UserController(DatabaseContext context)
        {
            this._context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers() 
        {
            var users = await _context.Set<ApplicationUser>().ToListAsync();
            return Ok(users);
        }
        public async Task<ActionResult<ApplicationUser>> GetUser(Guid id) 
        {
            var users = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.Id == id);
            return Ok(users);
        }
    }
}
