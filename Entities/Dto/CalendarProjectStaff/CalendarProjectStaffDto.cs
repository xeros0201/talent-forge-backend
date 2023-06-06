using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.CalendarProjectStaff
{
    public class CalendarProjectStaffDto
    {
        public DateTime Date { get; set; }
        public DayStatus DayStatus { get; set; }

        public bool IsHoliday { get; set; }

        public int ProjectId { get; set; }

        public BBProjectDto Project { get; set; }

        public int StaffId { get; set; }
        public StaffDto Staff { get; set; }
    }
}
