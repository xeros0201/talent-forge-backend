using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.CalendarProjectStaff;
using TFBackend.Entities.Dto.Role;
using TFBackend.Interfaces;
using TFBackend.Models;
using TFBackend.Repository;


namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICalendarRepository _calendarRepository;

        public  CalendarController(ApplicationDbContext context,ICalendarRepository calendarRepository , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _calendarRepository = calendarRepository;
        }
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CalendarProjectStaff))]
        public IActionResult CreateCalendar(List<CalendarProjectStaffPostDto> objDtos)
        {
            if (objDtos == null || objDtos.Count() < 1)
            {
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            }


            foreach(var objDto in objDtos)
            {

            var projectCheck = ProjectCheck(objDto.ProjectId);
            var staffCheck = StaffCheck(objDto.StaffId);
            if (!projectCheck)
                return CustomResult("One of the Project Id does not exist", System.Net.HttpStatusCode.BadRequest);
            if (!staffCheck)
                return CustomResult("One of the Staff Id does not exist", System.Net.HttpStatusCode.BadRequest);

            var createObj = _calendarRepository.Create(objDto);
            if (!createObj)
                return CustomResult("Create project failed", System.Net.HttpStatusCode.BadRequest);
            }

            if(!_calendarRepository.Save())
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success");

        }
        [HttpPut("array/")]
        public IActionResult PutAllProject(int projectId ,int staffId ,List<CalendarProjectStaffPostDto> objDtos)
        {

            var allCalendar = _context.CalendarProjectStaff.Where(c => c.ProjectId == projectId && c.StaffId == staffId);

            foreach(var calendar in allCalendar)
            {
            _context.CalendarProjectStaff.Remove(calendar);
            }
            foreach (var objDto in objDtos)
            {

                var projectCheck = ProjectCheck(objDto.ProjectId);
                var staffCheck = StaffCheck(objDto.StaffId);
                if (!projectCheck)
                    return CustomResult("One of the Project Id does not exist", System.Net.HttpStatusCode.BadRequest);
                if (!staffCheck)
                    return CustomResult("One of the Staff Id does not exist", System.Net.HttpStatusCode.BadRequest);

                var createObj = _calendarRepository.Create(objDto);
                if (!createObj)
                    return CustomResult("Create project failed", System.Net.HttpStatusCode.BadRequest);
            }

            if (!_calendarRepository.Save())
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success");

        }

        [HttpPut()]
        public IActionResult PutOneProject(int projectId, int staffId,DateTime Date, CalendarPut paypoad )
        {

            var cell = _context.CalendarProjectStaff.Where(c => c.ProjectId == projectId && c.StaffId == staffId && c.Date == Date).FirstOrDefault();

            
            if(paypoad.DayStatus != null)
            {
                cell.DayStatus = paypoad.DayStatus;
            }
            
            if (paypoad.IsHoliday != null)
            {
                cell.IsHoliday = paypoad.IsHoliday;
            }

            if (!_calendarRepository.Save())
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success");

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CalendarProjectStaff>))]
        public IActionResult GetAllCalendar()
        {
            var calendars = _mapper.Map<List<CalendarProjectStaffDto>>(_calendarRepository.Gets());

            if (!ModelState.IsValid)
                return CustomResult(ModelState, System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success", calendars);
        }

        private bool StaffCheck(int id)
        {
            return _context.Staff.Any(x  => x.Id == id);
        }


        private bool ProjectCheck(int id)
        {
            return _context.Projects.Any(x => x.Id == id);
        }

    }
}

