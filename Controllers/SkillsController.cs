using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
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
    public class SkillsController : BaseController
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
            var skills = await _context.Skills.Select(skills => _mapper.Map<SkillsDto>(skills)).ToListAsync();
            return Ok(skills);
        }   

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }
            var skillDto = _mapper.Map<SkillsDto>(skill);

            return CustomResult("Success",skillDto);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillsPutDto skillDto)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.Id == id);
            if (skill == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
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
                    return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            return CustomResult("No content", System.Net.HttpStatusCode.NoContent);
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostSkill(SkillsPostDto skillsDto)
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
                return CustomResult("Success",skills);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return CustomResult("No content",System.Net.HttpStatusCode.NoContent);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
