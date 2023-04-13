using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BBProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BBProjectsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BBProjectDto>>> GetProjects()
        {
            var projects = from p in _context.Projects
                           select new BBProjectDto()
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Description = p.Description,
                               Client = p.client.Name,
                               StartDate = p.StartDate,
                               EndDate = p.EndDate,
                               Location = p.Location.Name,
                               Department = p.Department.Name,
                               Active = p.Active,
                               Skills = (List<SkillsDto>)(from k in _context.ProjectSkills.Where(k => k.ProjectId == p.Id).Select(k => k.Skill) select
                                           new SkillsDto()
                                           {
                                               Id = k.Id,
                                               Name = k.Name,
                                               Color = k.Color,
                                           }).ToList(),
                               
                               Staff = (List<StaffDto>)(from s in _context.ProjectStaff.Where(s => s.ProjectId == p.Id).Select(s => s.Staff)
                                                        select
                                    new StaffDto()
                                    {
                                        Id = s.Id,
                                        Name = s.Name,
                                        Picture = s.Picture,
                                        Available = s.Available,
                                        AvailableDate = s.AvailableDate,
                                        Role = _context.Roles.FirstOrDefault(r => r.Id == s.RoleId).Name,
                                        skills = (List<SkillsDto>)(from k in _context.StaffSkills.Where(k => k.StaffId == s.Id).Select(k => k.Skill)
                                                                   select
                                                                   new SkillsDto()
                                                                   {
                                                                       Id = k.Id,
                                                                       Name = k.Name,
                                                                        Color = k.Color,
                                                                   }).ToList()
                                    }).ToList()
                           };
            return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BBProject>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }
            try
            {
                //check client ID. If client Id not null, find client Name
                string client = null;
                if(project.ClientId != null)
                {
                    client = _context.Client.FirstOrDefault(c => c.Id == project.ClientId).Name;
                }

                //check department ID. If department Id not null, find department Name
                string department = null;
                if (project.DepartmentId != null)
                {
                    department = _context.Departments.FirstOrDefault(d => d.Id == project.DepartmentId).Name;
                }
                //check location ID. If location Id not null, find location Name
                string location = null;
                if (project.LocationId != null)
                {
                    location = _context.Locations.FirstOrDefault(l => l.Id == project.LocationId).Name;
                }


                var projectDto = new BBProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Client = client,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Location = location,
                    Department = department,
                    Active = project.Active,
                    
                    Skills = (List<SkillsDto>)(from k in _context.ProjectSkills.Where(k => k.ProjectId == project.Id).Select(k => k.Skill)
                                               select
                                               new SkillsDto()
                                               {
                                                   Id = k.Id,
                                                   Name = k.Name,
                                                   Color = k.Color,
                                               }).ToList(),
                    Staff = (List<StaffDto>)(from s in _context.ProjectStaff.Where(s => s.ProjectId == project.Id).Select(s => s.Staff) select
                         new StaffDto()
                         {
                             Id = s.Id,
                             Name = s.Name,
                             Picture = s.Picture,
                             Available = s.Available,
                             AvailableDate = s.AvailableDate,
                             Role = _context.Roles.FirstOrDefault(r => r.Id == s.RoleId).Name,
                             skills = (List<SkillsDto>)(from k in _context.StaffSkills.Where(k => k.StaffId == s.Id).Select(k => k.Skill)
                                                        select
                                                        new SkillsDto()
                                                        {
                                                            Id = k.Id,
                                                            Name = k.Name,
                                                            Color = k.Color,
                                                        }).ToList()
                         }).ToList()
                    

                };
                return Ok(projectDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, BBProjectPutDto projectDto)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (id != project.Id)
            {
                return BadRequest();
            }

            //_context.Entry(project).State = EntityState.Modified;
            if(projectDto.Name != "")
            {
                project.Name = projectDto.Name;
            }
            if (projectDto.Description != "")
            {
                project.Description = projectDto.Description;
            }
            if (projectDto.ClientId != 0)
            {
                project.ClientId = projectDto.ClientId;
            }
            if(projectDto.StartDate != "")
            {
                project.StartDate = projectDto.StartDate;
            }
            if(projectDto.DepartmentId != 0)
            {
                project.DepartmentId= projectDto.DepartmentId;
            }
            if(projectDto.LocationId!= 0)
            {
                project.LocationId = projectDto.LocationId;
            }
            if(projectDto.EndDate != "")
            {
                project.EndDate = projectDto.EndDate;
            }
            if(projectDto.Active != "")
            {
                project.Active = projectDto.Active;
            }
            
            //To update many to many relationship, all related rows in database must be clear first, then add new rows            
            if(projectDto.StaffIds != null)
            {
                //clear related rows
                var projectstaffs = _context.ProjectStaff.Where(ps => ps.ProjectId == id).ToList();
                foreach(var projectstaff in projectstaffs)
                {
                    _context.ProjectStaff.Remove(projectstaff);
                }

                //add staff to projectstaff table
                foreach(int staffId in projectDto.StaffIds)
                {
                    var projectstaff = new ProjectStaff()
                    {
                        ProjectId = project.Id,
                        Project = _context.Projects.FirstOrDefault(p => p.Id == project.Id),
                        StaffId = staffId,
                        Staff = _context.Staff.FirstOrDefault(s => s.Id == staffId)
                    };
                    try
                    {
                        await _context.ProjectStaff.AddAsync(projectstaff);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return BadRequest(e.Message);
                    }
                };
            }
            if (projectDto.SkillsIds != null)
            {
                //clear related rows
                var projectskills = _context.ProjectSkills.Where(ps=>ps.ProjectId== id).ToList();
                foreach (var projectskill in projectskills)
                {
                    _context.ProjectSkills.Remove(projectskill);
                }

                //add skills to projectskills table
                foreach(int skillId in projectDto.SkillsIds)
                {
                    var projectskill = new ProjectSkill()
                    {
                        ProjectId = project.Id,
                        Project = _context.Projects.FirstOrDefault(p => p.Id == project.Id),
                        SkillId = skillId,
                        Skill = _context.Skills.FirstOrDefault(s => s.Id == skillId)
                    };
                    try
                    {
                        await _context.ProjectSkills.AddAsync(projectskill);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return BadRequest(e.Message);
                    }
                }

            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BBProject>> PostProject(BBProjectPostDto projectDto)
        {
            var project = new BBProject()
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                ClientId = projectDto.ClientId,
                StartDate = projectDto.StartDate,
                DepartmentId = projectDto.DepartmentId,
                LocationId = projectDto.LocationId,
                EndDate = projectDto.EndDate,
                Active = projectDto.Active

            };


            //project and skills
            //check skillids exist
            bool skillids_exist = false;
            bool skillids_count = false;

            if(projectDto.SkillIds.Count > 0)
            {
                skillids_count= true;
                foreach(int skill_id in projectDto.SkillIds)
                {
                    if(_context.Skills.Find(skill_id) != null)
                    {
                        skillids_exist= true;
                    }
                    else { skillids_exist = false; }
                }
                if(skillids_exist == false) { return BadRequest("One of the Skill Id does not exist"); }
            }
            //project and staff
            //check staffids exist
            bool staffids_exist = false;
            bool staffids_count = false;

            if(projectDto.StaffIds.Count > 0)
            {
                staffids_count= true;
                foreach(int staffId in projectDto.StaffIds)
                {
                    if(_context.Staff.Find(staffId) != null)
                    {
                        staffids_exist= true;
                    }else { staffids_exist = false; }
                }
                if(staffids_exist == false) { return BadRequest("One of the Staff Id does not exist"); }
            }

            //add project, projectskill and projectstaff to database

            try
            {
                await _context.Projects.AddAsync(project);
                var result = await _context.SaveChangesAsync();
                if(result== 1)
                {
                    //add projectskill
                    if (skillids_count && skillids_exist)
                    {
                        foreach(int skillId in projectDto.SkillIds)
                        {
                            var projectskill = new ProjectSkill()
                            {
                                SkillId = skillId,
                                Skill = _context.Skills.FirstOrDefault(s => s.Id == skillId),
                                ProjectId = project.Id,
                                Project = _context.Projects.FirstOrDefault(p => p.Id == project.Id)
                            };
                            try
                            {
                                await _context.ProjectSkills.AddAsync(projectskill);
                                var result_ProjectSkill = await _context.SaveChangesAsync();
                            }
                            catch(Exception e) 
                            {
                                Console.WriteLine(e.Message);
                                return BadRequest(e.Message);
                            }
                        }
                    }

                    //add projectstaff
                    if(staffids_count && staffids_exist)
                    {
                        foreach(int staffId in projectDto.StaffIds)
                        {
                            var projectstaff = new ProjectStaff()
                            {
                                StaffId = staffId,
                                Staff = _context.Staff.FirstOrDefault(s => s.Id == staffId),
                                ProjectId = project.Id,
                                Project = _context.Projects.FirstOrDefault(p => p.Id == project.Id)
                            };
                            try
                            {
                                await _context.ProjectStaff.AddAsync(projectstaff);
                                var result_ProjectStaff = await _context.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                return BadRequest(e.Message);
                            }
                        }
                    }
                }
                return Ok(projectDto);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
