using TFBackend.Models;

namespace TFBackend.Entities.Dto.CalendarProjectStaff
{
    public class CalendarPut
    {
       
        public DayStatus DayStatus { get; set; }

        public bool IsHoliday { get; set; }
 
    }
}
