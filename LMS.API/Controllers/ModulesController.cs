using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.API.Data;
using LMS.API.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using LMS.API.Models.Dtos;

namespace LMS.API.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ModulesController(IMapper mapper, DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleManipulationDto>>> GetModules()
        {
            return await _mapper.ProjectTo<ModuleManipulationDto>(_context.Modules).ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("courseid/{id}")]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule(Guid id)
        {
            var moduleObjc = await _context.Modules
                                           .Where(md => md.CourseId == id)
                                           .Include(md => md.Activities)
                                           .ToListAsync();

            if (moduleObjc == null)
            {
                return NotFound();
            }

            List<ModuleDto> dto = new();

            foreach (var module in moduleObjc)
            {
                var dtoObj = _mapper.Map<ModuleDto>(module);
                dto.Add(dtoObj);
            }

            return dto;
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(Guid id, ModuleManipulationDto module)
        {
            var moduleObj = await _context.Modules.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (moduleObj == null) return BadRequest();

            if (id != moduleObj.Id) return BadRequest();

            _mapper.Map(module, moduleObj);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleManipulationDto>> PostModule(ModuleForUpdateDto module)
        {
            var moduleObj = _mapper.Map<Module>(module);
            _context.Modules.Add(moduleObj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = moduleObj.Id }, _mapper.Map<ModuleManipulationDto>(moduleObj));
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(Guid id)
        {
            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(Guid id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
    }
}
