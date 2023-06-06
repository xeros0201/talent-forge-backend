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
using TFBackend.Entities.Dto.Department;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            
            var departments = _context.Departments.Select(departments => _mapper.Map<DepartmentDto>(departments));
            return Ok(departments);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }
            var departmentDto = _mapper.Map<DepartmentDto>(department);


            return CustomResult("Success",departmentDto);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentPutDto departmentDto)
        {
            var department = _context.Departments.FirstOrDefault(d=>d.Id == id);
            if ( department == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

           // _context.Entry(department).State = EntityState.Modified;
           department.Name = departmentDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDepartment(DepartmentPostDto departmentDto)
        {
            var department = new Department()
            {
                Name = departmentDto.Name
            };

            try
            {
                await _context.Departments.AddAsync(department);
                var result = await _context.SaveChangesAsync();
                return CustomResult("Success",department);
            }
            catch(Exception e)
            {
                Console.WriteLine("Something bad happened!");
                Console.WriteLine(e.Message);

                return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Departments/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }
            //change all project with corresponding departmentId to null
            List<BBProject> project_list = _context.Projects.Where(p => p.DepartmentId == id).ToList();
            foreach (var project in project_list)
            {
                project.DepartmentId = null;
                try
                {
                    _context.Entry(project).State = EntityState.Modified;
                    var project_result = await _context.SaveChangesAsync();
                }
                catch (Exception e) { return CustomResult(e.Message,System.Net.HttpStatusCode.BadRequest); }
            }
            

            //delete department
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return CustomResult("No content", System.Net.HttpStatusCode.NoContent);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
