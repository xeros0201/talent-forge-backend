using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.CalendarProjectStaff;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Interfaces;
using TFBackend.Models;

namespace TFBackend.Repository
{
    public class CalendarRepository : ICalendarRepository
    {

        private readonly ApplicationDbContext _context;
        public CalendarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(CalendarProjectStaffPostDto obj)
        {

            try
            {
                var newObj = new CalendarProjectStaff()
                {
                    Date = obj.Date,
                    DayStatus = obj.DayStatus,
                    IsHoliday = obj.IsHoliday, 
                    ProjectId   = obj.ProjectId,
                    StaffId = obj.StaffId,
                };

                _context.CalendarProjectStaff.Add(newObj);
                return true;


            }
            catch (Exception ex)
            {
                 return false;
            }

 
        }

        public ICollection<CalendarProjectStaff> Gets()
        {
           
                return _context.CalendarProjectStaff
                    .Include(x => x.Project)
                        .ThenInclude(xx => xx.ProjectStaff).ThenInclude(xxx => xxx.Staff)
                    .Include(x => x.Staff)
                        .ThenInclude(xx => xx.Role)
                    .Include(x => x.Staff)
                        .ThenInclude(xx => xx.StaffSkills)
                            .ThenInclude(xxx => xxx.Skill).ToList();
           
          
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
 
    }
}
