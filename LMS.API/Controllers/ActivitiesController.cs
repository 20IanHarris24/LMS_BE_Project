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
    [Route("api/activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivitiesController(IMapper mapper, DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityListDto>>> GetActivities()
        {
            var activitiesList = await _context.Activities
                .Include(act => act.ActivityType)
                .ToListAsync();

            List<ActivityListDto> dto = new();
            foreach (var activity in activitiesList) { 
                var dtoObj = _mapper.Map<ActivityListDto>(activity);
                dto.Add(dtoObj);
            }
            return dto;
        }

        // GET: api/Activities/5
        [HttpGet("moduleid/{id}")]
        public async Task<ActionResult<IEnumerable<ActivityListDto>>> GetActivityByModuleId(Guid id)
        {
            try
            {
                // Fetch activities for the specified module
                var actList = await _context.Activities
                    .Where(act => act.ModuleId == id)
                    .Include(act => act.ActivityType)
                    .AsNoTracking()
                    .ToListAsync();

                // Check if any activities were found
                if (!actList.Any())
                {
                    return NotFound("No activities found for the specified module.");
                }

                // Map activities to DTO
                var dto = _mapper.Map<List<ActivityListDto>>(actList);
                return Ok(dto); // Explicitly return OK with the DTO
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                // _logger.LogError(ex, "Error fetching activities for module {ModuleId}", id);

                return StatusCode(500, "Internal server error occurred while processing the request.");
            }
        }



        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(Guid id, ActivityPutDto activity)
        {
            var actObj = _context.Activities.Where(actObj => actObj.Id == id).FirstOrDefault();

            if (actObj == null) return BadRequest();

            if (id != actObj.Id) return BadRequest();

            //_mapper.Map(module, moduleObj);

            _mapper.Map(activity, actObj);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityListDto>> PostActivity(ActivityPutDto activity)
        {
            var actObj = _mapper.Map<Activity>(activity);
            _context.Activities.Add(actObj);
            await _context.SaveChangesAsync();

            return Created();
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(Guid id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
