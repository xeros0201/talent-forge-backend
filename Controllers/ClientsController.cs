using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            
            
            var client = from c in _context.Client
                         select new ClientDto
                         {
                             Id = c.Id,
                             Name = c.Name,
                             Active = c.Active,
                             LastUpdated = c.LastUpdated,
                             TotalProjects = _context.Projects.Where(p=>p.ClientId == c.Id).Select(p=>p.Id).Count().ToString(),
                             Staff = (List<StaffDto>)(from s in _context.StaffClients.Where(s => s.ClientId == c.Id).Select(s => s.Staff) select
                                    new StaffDto()
                                    {
                                        Id = s.Id,
                                        Name = s.Name,
                                        Picture = s.Picture,
                                        Available = s.Available,
                                        AvailableDate = s.AvailableDate,
                                        Roll = _context.Rolls.FirstOrDefault(r => r.Id == s.RollId).Name,
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
            
            return Ok(client);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            List<StaffDto> staff = (List<StaffDto>)(from s in _context.StaffClients.Where(s => s.ClientId == id).Select(s => s.Staff) select
                new StaffDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Picture= s.Picture,
                    Available= s.Available,
                    AvailableDate= s.AvailableDate,
                    Roll = _context.Rolls.FirstOrDefault(r => r.Id == s.RollId).Name,
                    skills = (List<SkillsDto>)(from k in _context.StaffSkills.Where(k => k.StaffId == s.Id).Select(k => k.Skill) select
                                               new SkillsDto()
                                               {
                                                   Id = k.Id,
                                                   Name = k.Name,
                                                   Color = k.Color,
                                               }).ToList()
                }).ToList();
            //get StaffClient table for staff IDs

            var clientDto = new ClientDto()
            {
                Id = client.Id,
                Name = client.Name,
                Active = client.Active,
                LastUpdated = client.LastUpdated,
                TotalProjects = client.TotalProjects,
                Staff = staff
            };

            return Ok(clientDto);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientPutDto clientDto)
        {
            var client = _context.Client.FirstOrDefault(c => c.Id == id);
            if (id != client.Id)
            {
                return BadRequest();
            }

            //_context.Entry(client).State = EntityState.Modified;
            if(clientDto.Name != "")
            {
                client.Name = clientDto.Name;
            }
            if (clientDto.Active != "")
            {
                client.Active = clientDto.Active;
            }
            if (clientDto.LastUpdated != "")
            {
                client.LastUpdated = clientDto.LastUpdated;
            }

            //To update many to many relationship, all related rows in database must be clear first, then add new rows
            if (clientDto.StaffIds.Count > 0)
            {
                //clear related rows
                var staffclients = _context.StaffClients.Where(sc => sc.ClientId == id).ToList();
                foreach (var staffclient in staffclients)
                {
                    _context.StaffClients.Remove(staffclient);
                }

                //add staff to staffclient table
                foreach (int staff_id in clientDto.StaffIds)
                {
                    var staffclient = new StaffClient()
                    {
                        ClientId = id,
                        Client = _context.Client.FirstOrDefault(c => c.Id == client.Id),
                        StaffId = staff_id,
                        Staff = _context.Staff.FirstOrDefault(s => s.Id == staff_id)
                    };
                    try
                    {
                        await _context.StaffClients.AddAsync(staffclient);
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
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientPostDto clientDto)
        {
            //create new client
            var client = new Client()
            {
                Name = clientDto.Name,
                Active = clientDto.Active,
                LastUpdated= clientDto.LastUpdated,
            };

            //check staffids exist
            bool staffids_exist = false;
            bool staffids_count = false;

            //check if staff assign to new client
            if (clientDto.StaffIds.Count() > 0)
            {
                staffids_count = true;
                
                //loop through each staff id and check if id exists
                foreach(int id in clientDto.StaffIds)
                {
                    if(_context.Staff.Find(id)!= null)
                    {
                        staffids_exist= true;
                    }
                    else
                    {
                        staffids_exist = false;
                    }
                }
                if (staffids_exist == false) { return BadRequest("One of the Staff Id does not exist"); }
            }

            //add staff to staffclient
            try
            {
                await _context.Client.AddAsync(client);
                var result = await _context.SaveChangesAsync();
                if(result == 1)
                {
                    if(staffids_exist && staffids_count)
                    {
                        foreach(int staff_id in clientDto.StaffIds)
                        {
                            var staffclient = new StaffClient()
                            {
                                ClientId = client.Id,
                                Client = _context.Client.FirstOrDefault(c => c.Id == client.Id),
                                StaffId = staff_id,
                                Staff = _context.Staff.FirstOrDefault(s => s.Id == staff_id),
                            };
                            try
                            {
                                await _context.StaffClients.AddAsync(staffclient);
                                var result_staffskill = await _context.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                return BadRequest(e.Message);
                            }
                        }
                    }

                }
                return Ok(clientDto);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }

        }

        // DELETE: api/Clients/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            //change all project with corresponding clientId to null
            List<BBProject> project_list = _context.Projects.Where(p=>p.ClientId == id).ToList();
            foreach (var project in project_list)
            {
                project.ClientId = null;
                try
                {
                    _context.Entry(project).State = EntityState.Modified;
                    var project_result = await _context.SaveChangesAsync();
                }
                catch (Exception e) { return BadRequest(e.Message); }
            }

            //delete client
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
