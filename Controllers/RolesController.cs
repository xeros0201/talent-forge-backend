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
using TFBackend.Entities.Dto.Role;
using TFBackend.Models;


namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
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
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = _context.Roles.Select(role => _mapper.Map<RolesDto>(role));
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoll(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var rollDto = _mapper.Map<RolesDto>(role);

            return Ok(rollDto);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RolesPutDto roleDto)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (id != role.Id)
            {
                return BadRequest();
            }

            role.Name = roleDto.Name;

            try
            {
                var rollUpdate = await _context.SaveChangesAsync();
                return Ok(roleDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // POST: api/Rolls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RolesPostDto roleDto)
        {
            var role = new Role
            {
                Name = roleDto.Name
            };

            try
            {
                await _context.Roles.AddAsync(role);
                var result = await _context.SaveChangesAsync();
                return Ok(role);
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
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
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
                catch (Exception e) { return BadRequest(e.Message); }
            }

            //delete role
            _context.Roles.Remove(role); 
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
