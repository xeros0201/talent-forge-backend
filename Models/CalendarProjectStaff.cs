using MessagePack;
using TFBackend.Entities.Dto.Staff;

namespace TFBackend.Models
{
    public class CalendarProjectStaff
    {
        //[Key] 


        public BBProject Project { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public DateTime Date { get; set; }
        public DayStatus DayStatus { get; set; }
       
        public bool  IsHoliday { get; set; }
    
        public int ProjectId { get; set; }

 
    }

    public enum DayStatus {
        Billable = 0,
        Tentative =1,
        Leave =2,
        Non_billable=3,

    }
}
