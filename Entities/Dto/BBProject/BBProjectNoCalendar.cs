using TFBackend.Entities.Dto.CalendarProjectStaff;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Entities.Dto.Department;
using TFBackend.Entities.Dto.Location;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;

namespace TFBackend.Entities.Dto.BBProject
{
    public class BBProjectNoCalendar
    {
        public List<SkillsDto> ProjectSkill { get; set; }
      
   
        public string Name { get; set; }
        public string? Color { get; set; }
        public int MangerId { get; set; }
        public string Description { get; set; }
        public ClientWithProjectDto client { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public LocationDto Location { get; set; }
        public DepartmentDto Department { get; set; }
        public string Active { get; set; }
        public int Id { get; set; }
    }
}
