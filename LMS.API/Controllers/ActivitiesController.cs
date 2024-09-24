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
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivitiesController(IMapper mapper, DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityListDto>>> GetActivitys()
        {
            var actList = await _context.Activitys
                                    .ToListAsync();


            List<ActivityListDto> activities = new List<ActivityListDto>();
            if (actList.Any()) {
                foreach (var activity in actList)
                {
                    var type = await _context.ActivityType.Where(act => act.Id == activity.TypeId).FirstOrDefaultAsync();
                    var dto = new ActivityListDto();
                    dto.Name = activity.Name;
                    dto.Description = activity.Description;
                    dto.ActivityType = type?.Name;
                    dto.Start = activity.Start;
                    dto.End = activity.End;
                    dto.ModuleId = activity.ModuleId;
                    activities.Add(dto);
                }
            }

            return activities;
        }

        //GET: api/Activities/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Activity>> GetActivity(Guid id)
        //{
        //    var activity = await _context.Activitys.FindAsync(id);

        //    if (activity == null)
        //    {
        //        return NotFound();
        //    }

        //    return activity;
        //}

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(Guid id, ActivityPutDto activity)
        {
            if (id != activity.Id) return BadRequest();

            var activityToModify = await _context.Activitys.FirstOrDefaultAsync(activity => activity.Id == id);

            if (activityToModify == null) return BadRequest();

            _mapper.Map(activity, activityToModify);

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
        public async Task<ActionResult<Activity>> PostActivity(ActivityDto activity)
        {
            var actObj = _mapper.Map<Activity>(activity);
            _context.Activitys.Add(actObj);
            await _context.SaveChangesAsync();

            return Created();

            //return CreatedAtAction("GetActivity", new { id = actObj.Id }, actObj);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var activity = await _context.Activitys.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activitys.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(Guid id)
        {
            return _context.Activitys.Any(e => e.Id == id);
        }
    }
}
