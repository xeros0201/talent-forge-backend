using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Role;
using TFBackend.Models;


namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _context.Roles.Select(role => _mapper.Map<RolesDto>(role));
            return CustomResult("Success",roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoll(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

            var rollDto = _mapper.Map<RolesDto>(role);

            return CustomResult("Success",rollDto);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RolesPutDto roleDto)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

            role.Name = roleDto.Name;

            try
            {
                var rollUpdate = await _context.SaveChangesAsync();
                return CustomResult("Success",roleDto);
            }
            catch (Exception e)
            {
                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
            }

        }

        // POST: api/Rolls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRole(RolesPostDto roleDto)
        {
            var role = new Role
            {
                Name = roleDto.Name
            };

            try
            {
                await _context.Roles.AddAsync(role);
                var result = await _context.SaveChangesAsync();
                return CustomResult("Success",role);
            }
            catch (Exception e)
            {
                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
            }
        }


        // DELETE: api/Rolls/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteRoll(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

            //change all staff with corresponing rollId to null
            List<Staff> staff_list = _context.Staff.Where(s => s.RoleId == id).ToList();

            foreach(var staff in staff_list)
            {
                staff.RoleId = null;
                try
                {
                    _context.Entry(staff).State = EntityState.Modified;
                    var staff_result = await _context.SaveChangesAsync();
                }
                catch (Exception e) { return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest); }
            }

            //delete role
            _context.Roles.Remove(role); 
            await _context.SaveChangesAsync();

            return CustomResult("No content",System.Net.HttpStatusCode.NoContent);
        }
    }
}
