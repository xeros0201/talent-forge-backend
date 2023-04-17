using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Department;
using TFBackend.Entities.Dto.Location;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : BaseController
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
        public async Task<IActionResult> GetLocations()
        {
            var locations = _context.Locations.Select(locations => _mapper.Map<LocationDto>(locations));
            return CustomResult("Success",locations);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }
            var locationDto = _mapper.Map<LocationDto>(location);

            return CustomResult("Success", locationDto);
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationPutDto locationDto)
        {
            var location = _context.Locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
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
                    return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            return CustomResult("No content",System.Net.HttpStatusCode.NoContent);
        }

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostLocation(LocationPostDto locationDto)
        {
            var location = new Models.Location
            {
                Name = locationDto.Name
            };
            try
            {
                await _context.Locations.AddAsync(location);
                var result = await _context.SaveChangesAsync();
                return CustomResult("Success",location);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Locations/5
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
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
                catch(Exception e) { return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest); }
            }

            //delete location
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return CustomResult("No content",System.Net.HttpStatusCode.NoContent);
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
