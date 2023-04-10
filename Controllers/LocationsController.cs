using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Department;
using TFBackend.Entities.Dto.Location;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LocationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var locations = _context.Locations.Select(locations => _mapper.Map<LocationDto>(locations));
            return Ok(locations);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }
            var locationDto = _mapper.Map<LocationDto>(location);

            return Ok(locationDto);
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationPutDto locationDto)
        {
            var location = _context.Locations.FirstOrDefault(l => l.Id == id);
            if (id != location.Id)
            {
                return BadRequest();
            }

            //_context.Entry(location).State = EntityState.Modified;
            location.Name = locationDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(LocationPostDto locationDto)
        {
            var location = new Location()
            {
                Name = locationDto.Name
            };
            try
            {
                await _context.Locations.AddAsync(location);
                var result = await _context.SaveChangesAsync();
                return Ok(location);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Locations/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            //change all project with corresponding locationId to null
            List<BBProject> projects = _context.Projects.Where(p => p.LocationId == id).ToList();
            foreach(var project in projects)
            {
                project.LocationId = null;
                try
                {
                    _context.Entry(project).State= EntityState.Modified;
                    var project_result = await _context.SaveChangesAsync();
                }
                catch(Exception e) { return BadRequest(e.Message); }
            }

            //delete location
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
