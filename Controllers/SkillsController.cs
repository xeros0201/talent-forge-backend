using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Location;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SkillsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            var skills = _context.Skills.Select(skills => _mapper.Map<SkillsDto>(skills));
            return Ok(skills);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }
            var skillDto = _mapper.Map<SkillsDto>(skill);

            return Ok(skillDto);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillsPutDto skillDto)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.Id == id);
            if (id != skill.Id)
            {
                return BadRequest();
            }

            //_context.Entry(skill).State = EntityState.Modified;
            if(skillDto.Name != "")
            {
                skill.Name= skillDto.Name;
            }
            if(skillDto.Color != "")
            {
                skill.Color= skillDto.Color;
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(SkillsPostDto skillsDto)
        {
            var skills = new Skill()
            {
                Name = skillsDto.Name,
                Color = skillsDto.Color
            };
            try
            {
                await _context.Skills.AddAsync(skills);
                var result = await _context.SaveChangesAsync();
                return Ok(skills);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
