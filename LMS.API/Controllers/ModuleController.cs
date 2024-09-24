using AutoMapper;
using LMS.API.Data;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        public ModuleController(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules()
        {
            var modules = await _context.Set<Module>().Include(m => m.Activities).ToListAsync();
            var moduleDtos = _mapper.Map<IEnumerable<ModuleDto>>(modules);

            return Ok(moduleDtos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModuleDto>> UpdateModule(string id, ModuleForUpdateDto moduleDto)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id.ToString() == id);
            if (module == null)
            {
                return NotFound();
            }
            _mapper.Map(moduleDto, module);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ModuleForUpdateDto>(module));
        }
    }
}
