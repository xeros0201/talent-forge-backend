using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.CalendarProjectStaff
{
    public class CalendarNoStaff
    {
        public DateTime Date { get; set; }
        public DayStatus DayStatus { get; set; }

        public bool IsHoliday { get; set; }

        public int ProjectId { get; set; }

        public BBProjectNoCalendar Project { get; set; }

    
    }
}
