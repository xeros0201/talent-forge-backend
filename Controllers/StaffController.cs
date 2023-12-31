﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TFBackend.Data;
using TFBackend.Entities.Dto.Certs;
using TFBackend.Entities.Dto.Role;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Entities.Dto.StaffSkills;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : BaseController
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
        public async Task<IActionResult> GetStaff()
        {
 

            var staff = _mapper.Map< List<StaffDto>>( _context.Staff
                    .Include(s => s.Role)
                    .Include( s => s.StaffSkills)
                        .ThenInclude(st => st.Skill)
                     .Include(s => s.ProjectStaff)
                        .ThenInclude(st => st.Project)
                        .ToList());
                     
            if (staff == null)
            {
                List<StaffDto> nullStaff = new List<StaffDto>();
                return CustomResult("Success", nullStaff);
            }
            return  CustomResult("Success",staff);
        }

        [HttpGet("agendar/all")]
        public async Task<IActionResult> GetStaffsWithAgendar()
        {


            var staff = _mapper.Map<List<StaffOnlyCalendarDto>>(_context.Staff
                    .Include(s => s.Role)
                    .Include(s => s.CalendarProjectStaff).ThenInclude(ss => ss.Project)
                    .Include(s => s.StaffSkills)
                        .ThenInclude(st => st.Skill)
                     .ToList());

            if (staff == null)
            {
                List<StaffDto> nullStaff = new List<StaffDto>();
                return CustomResult("Success", nullStaff);
            }
            return CustomResult("Success", staff);
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaff(int id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Id == id);
            if (staff == null)
                return CustomResult($"Staff with    id{id} cannot be found",System.Net.HttpStatusCode.NotFound);

            var staffDto  =  _mapper.Map<StaffDto>(_context.Staff
                    .Include(s => s.Role)
                    .Include(s => s.StaffSkills)
                        .ThenInclude(st => st.Skill)
                     .Include(s => s.ProjectStaff)
                        .ThenInclude(st => st.Project)
                     .Include(st => st.StaffCerts).ThenInclude(c =>c.Cert).ThenInclude( cc => cc.CertCategory)
                
                        .Where(s =>s.Id == id)
                                .FirstOrDefault());
            return CustomResult("Success",staffDto);
        }
        [HttpGet("agendar/signle/{id}")]
        public async Task<IActionResult> GetStaffWithAgenda(int id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Id == id);
            if (staff == null)
                return CustomResult($"Staff with    id{id} cannot be found", System.Net.HttpStatusCode.NotFound);

            var staffDto = _mapper.Map<StaffOnlyCalendarDto>(_context.Staff
                    .Include(s => s.Role)
                     .Include(s => s.CalendarProjectStaff).ThenInclude(ss => ss.Project)
                    .Include(s => s.StaffSkills)
                        .ThenInclude(st => st.Skill)
                     .Where(s => s.Id == id)
                                .FirstOrDefault());
            return CustomResult("Success", staffDto);
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, StaffPutDto staffDto)
        {
            var staff = _context.Staff.FirstOrDefault(s => s.Id == id);
            if (staff == null)
            {
                return CustomResult("Not found",System.Net.HttpStatusCode.NotFound);
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
            if(staffDto.RoleId != 0)
            {
                staff.RoleId = staffDto.RoleId;
            }
            
            //To update many to many relationship, all related rows in database must be clear first, then add new rows

            if(staffDto.CertIds.Count > 0)
            {
                var staffCerts = _context.StaffCerts.Where(sc => sc.StaffId == id).ToList();
                foreach (var cert in staffCerts)
                {
                    _context.StaffCerts.Remove(cert);
                }
                foreach (StaffCertDto cert in staffDto.CertIds)
                {
                    var staffskill = new StaffCert()
                    {
                        StaffId = staff.Id,
                        Staff = _context.Staff.FirstOrDefault(s => s.Id == staff.Id),
                        CertId = cert.CertId,
                        Cert = _context.Certs.FirstOrDefault(k => k.Id == cert.CertId),
                        AcquiredDate = cert.AcquiredDate,
                        CertLink = cert.CertLink,
                        ExpiredDate = cert.ExpiredDate,
                        InterNationalId = cert.InterNationalId,
                        IssuingOrganisation = cert.IssuingOrganisation,
                        RenewalDate = cert.RenewalDate
                    };
                    try
                    {
                        await _context.StaffCerts.AddAsync(staffskill);
                        var result_staffskill = await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return CustomResult(e.Message, System.Net.HttpStatusCode.BadRequest);
                    }
                }

                }
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
                        return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
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
                    return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            return CustomResult("No content", System.Net.HttpStatusCode.NoContent);
        }

        // POST: api/Staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        [HttpPost]
        public async Task<IActionResult> PostStaff(StaffPostDto staffDto)
        {
            //create new staff

    
            var hashedPassword =  HashPassword.CreateHash(staffDto.Password);

            var staff = new Staff()
            {
                Name = staffDto.Name,
                Picture = staffDto.Picture,
                RoleId = staffDto.RoleId,
                Role = _context.Roles.FirstOrDefault(r => r.Id == staffDto.RoleId),
                Username = staffDto.Username,
                Password = hashedPassword,
                Available = staffDto.Available,
                AvailableDate = staffDto.AvailableDate
            };

            //check skillids exist
            bool skillids_exist = false;
            bool skillids_count = false;

            bool certids_exist = false;
            bool certids_count = false;


            if (staffDto.CertIds.Count > 0)
            {
                certids_count = true;
                foreach (StaffCertDto Cert in staffDto.CertIds)
                {
                    if (_context.Certs.Find(Cert.CertId) != null)
                    {
                        certids_exist = true;
                    }
                    else { certids_exist = false; }
                }
                if (certids_exist == false) { return BadRequest("One of the Cert  Id does not exist"); }
            }

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
                    if (certids_count && certids_exist)
                    {
                        foreach (StaffCertDto cert in staffDto.CertIds)
                        {
                            var staffskill = new StaffCert()
                            {
                                StaffId = staff.Id,
                                Staff = _context.Staff.FirstOrDefault(s => s.Id == staff.Id),
                                CertId = cert.CertId,
                                Cert = _context.Certs.FirstOrDefault(k => k.Id == cert.CertId),
                                AcquiredDate = cert.AcquiredDate,
                                CertLink = cert.CertLink,
                                ExpiredDate = cert.ExpiredDate,
                                InterNationalId = cert.InterNationalId, 
                                IssuingOrganisation = cert.IssuingOrganisation,
                                RenewalDate = cert.RenewalDate
                            };
                            try
                            {
                                await _context.StaffCerts.AddAsync(staffskill);
                                var result_staffskill = await _context.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
                            }
                        };

                    }
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
                                return CustomResult(e.Message, System.Net.HttpStatusCode.BadRequest);
                            }
                        };

                    }
                    return CustomResult("Success", staffDto);
                }
                return CustomResult("Success",staffDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return CustomResult("No content", System.Net.HttpStatusCode.NoContent);
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
