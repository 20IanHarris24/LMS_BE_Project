using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.API.Data;
using LMS.API.Models.Entities;
using LMS.API.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityTypesController(IMapper mapper, DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ActivityTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityType>>> GetActivityType()
        {
            return await _context.ActivityType.ToListAsync();
        }

        // GET: api/ActivityTypes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ActivityType>> GetActivityType(Guid id)
        //{
        //    var activityType = await _context.ActivityType.FindAsync(id);

        //    if (activityType == null)
        //    {
        //        return NotFound();
        //    }

        //    return activityType;
        //}

        // PUT: api/ActivityTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityType(Guid id, ActivityType activityType)
        {
            if (id != activityType.Id)
            {
                return BadRequest();
            }

            _context.Entry(activityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ActivityTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityType>> PostActivityType(ActivityTypeDto activityType)
        {
            var obj = _mapper.Map<ActivityType>(activityType);
            _context.ActivityType.Add(obj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityType", new { id = obj.Id }, activityType);
        }

        // DELETE: api/ActivityTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityType(Guid id)
        {
            var activityType = await _context.ActivityType.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }

            _context.ActivityType.Remove(activityType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityTypeExists(Guid id)
        {
            return _context.ActivityType.Any(e => e.Id == id);
        }
    }
}
