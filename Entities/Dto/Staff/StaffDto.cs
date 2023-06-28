using TFBackend.Entities.Dto.Skills;
using TFBackend.Models;
using TFBackend.Entities.Dto.Role;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.CalendarProjectStaff;
using TFBackend.Entities.Dto.Certs;

namespace TFBackend.Entities.Dto.Staff
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Picture { get; set; }
        public string Available { get; set; }
        public string AvailableDate { get; set; }
        public string Password { get; set; }
        //one to many realtionship
        public int? RoleId { get; set; }
        public RolesDto? Role { get; set; }

        //many to many relationship

        public List<SkillsDto>? StaffSkills { get; set; }

        public List<CertNoStaffDto> StaffCerts { get; set; }

        public List<BBProjectDto>? ProjectStaff { get; set; }

        public List<CalendarProjectStaffDto>? CalendarProjectStaff { get; set;}

    }
}
