using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TFBackend.Data;
using TFBackend.Entities.Dto.Roll;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Entities.Dto.StaffSkills;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StaffController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            //var staff = _context.Staff.Select(staff => _mapper.Map<StaffDto>(staff));

            var staff = from s in _context.Staff
                        select new StaffDto()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Picture = s.Picture,
                            Available = s.Available,
                            AvailableDate = s.AvailableDate,
                            Roll = _context.Rolls.FirstOrDefault(r => r.Id == s.RollId).Name,
                            skills = (List<SkillsDto>)(from k in _context.StaffSkills.Where(k => k.StaffId == s.Id).Select(k => k.Skill) select 
                                     new SkillsDto()
                            {
                                Id = k.Id,
                                Name = k.Name,
                                Color = k.Color,
                            })
                        };
            return Ok(staff);
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = _context.Staff.FirstOrDefault(s => s.Id == id);
            if (staff == null)
                return NotFound($"Staff with id{id} cannot be found");

            var staffDto = new StaffDto()
            {
                Id = staff.Id,
                Name = staff.Name,
                Picture = staff.Picture,
                Available = staff.Available,
                AvailableDate = staff.AvailableDate,
                Roll = _context.Rolls.FirstOrDefault(r => r.Id == staff.RollId).Name,
                skills = (List<SkillsDto>)(from k in _context.StaffSkills.Where(k=>k.StaffId == id).Select(k=>k.Skill)select
                                           new SkillsDto()
                                           {
                                               Id = k.Id,
                                               Name = k.Name,
                                               Color = k.Color,
                                           }).ToList()
            };
            return Ok(staffDto);
            
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, StaffPutDto staffDto)
        {
            var staff = _context.Staff.FirstOrDefault(s => s.Id == id);
            if (id != staff.Id)
            {
                return BadRequest();
            }

            
            //_context.Entry(staff).State = EntityState.Modified;

            if(staffDto.Name != "")
            {
                staff.Name = staffDto.Name;
            }
            if(staffDto.Picture != "")
            {
                staff.Picture = staffDto.Picture;
            }
            if (staffDto.Available != ""){
                staff.Available = staffDto.Available;
            }
            if(staffDto.AvailableDate!= "")
            {
                staff.AvailableDate = staffDto.AvailableDate;
            }
            if(staffDto.RollId != 0)
            {
                staff.RollId = staffDto.RollId;
            }
            
            //To update many to many relationship, all related rows in database must be clear first, then add new rows
            if (staffDto.SkillIds.Count > 0)
            {
                //clear related rows
                var staffskills = _context.StaffSkills.Where(ss=>ss.StaffId == id).ToList();
                foreach(var staffskill in staffskills)
                {
                    _context.StaffSkills.Remove(staffskill);
                }
                
                

                //add skills to staffskill table
                foreach (int skill_id in staffDto.SkillIds)
                {
                    var staffskill = new StaffSkills()
                    {
                        StaffId = staff.Id,
                        Staff = _context.Staff.FirstOrDefault(s => s.Id == staff.Id),
                        SkillId = skill_id,
                        Skill = _context.Skills.FirstOrDefault(k => k.Id == skill_id)
                    };
                    try
                    {
                        await _context.StaffSkills.AddAsync(staffskill);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return BadRequest(e.Message);
                    }
                };

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

        // POST: api/Staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(StaffPostDto staffDto)
        {
            //create new staff
            var staff = new Staff()
            {
                Name = staffDto.Name,
                Picture = staffDto.Picture,
                RollId = staffDto.RollId,
                Roll = _context.Rolls.FirstOrDefault(r => r.Id == staffDto.RollId),
                Available = staffDto.Available,
                AvailableDate = staffDto.AvailableDate,
            };

            //check skillids exist
            bool skillids_exist = false;
            bool skillids_count = false;
            if (staffDto.SkillIds.Count > 0)
            {
                skillids_count = true;
                foreach (int id in staffDto.SkillIds)
                {
                    if (_context.Skills.Find(id) != null)
                    {
                        skillids_exist = true;
                    }
                    else { skillids_exist = false; }
                }
                if (skillids_exist == false) { return BadRequest("One of the Skill Id does not exist"); }
            }

            //add staff, staffclient and staffskills to database
            try
            {

                await _context.Staff.AddAsync(staff);
                var result = await _context.SaveChangesAsync();

                //add to mamy-to-mamy table: StaffSkills
                if (result == 1)
                {
                    if (skillids_count && skillids_exist)
                    {
                        foreach (int skill_id in staffDto.SkillIds)
                        {
                            var staffskill = new StaffSkills()
                            {
                                StaffId = staff.Id,
                                Staff = _context.Staff.FirstOrDefault(s => s.Id == staff.Id),
                                SkillId = skill_id,
                                Skill = _context.Skills.FirstOrDefault(k => k.Id == skill_id)
                            };
                            try
                            {
                                await _context.StaffSkills.AddAsync(staffskill);
                                var result_staffskill = await _context.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                return BadRequest(e.Message);
                            }
                        };

                    }
                    return Ok(staffDto);
                }
                return Ok(staffDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
