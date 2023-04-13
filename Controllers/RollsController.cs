using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Roll;
using TFBackend.Models;


namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RollsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rolls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roll>>> GetRolls()
        {
            var rolls = _context.Rolls.Select(rolls => _mapper.Map<RollsDto>(rolls));
            return Ok(rolls);
        }

        // GET: api/Rolls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roll>> GetRoll(int id)
        {
            var roll = await _context.Rolls.FindAsync(id);

            if (roll == null)
            {
                return NotFound();
            }

            var rollDto = _mapper.Map<RollsDto>(roll);

            return Ok(rollDto);
        }

        // PUT: api/Rolls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoll(int id, RollsPutDto rollDto)
        {
            var roll = _context.Rolls.FirstOrDefault(r => r.Id == id);
            if (id != roll.Id)
            {
                return BadRequest();
            }

            roll.Name = rollDto.Name;

            try
            {
                var rollUpdate = await _context.SaveChangesAsync();
                return Ok(rollDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // POST: api/Rolls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roll>> PostRoll(RollsPostDto rollDto)
        {
            var roll = new Roll
            {
                Name = rollDto.Name
            };

            try
            {
                await _context.Rolls.AddAsync(roll);
                var result = await _context.SaveChangesAsync();
                return Ok(roll);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // DELETE: api/Rolls/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteRoll(int id)
        {
            var roll = await _context.Rolls.FindAsync(id);
            if (roll == null)
            {
                return NotFound();
            }

            //change all staff with corresponing rollId to null
            List<Staff> staff_list = _context.Staff.Where(s => s.RollId == id).ToList();

            foreach(var staff in staff_list)
            {
                staff.RollId = null;
                try
                {
                    _context.Entry(staff).State = EntityState.Modified;
                    var staff_result = await _context.SaveChangesAsync();
                }
                catch (Exception e) { return BadRequest(e.Message); }
            }

            //delete roll
            _context.Rolls.Remove(roll); 
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
