using TFBackend.Entities.Dto.Skills;
using TFBackend.Models;
using TFBackend.Entities.Dto.Role;
namespace TFBackend.Entities.Dto.Staff
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Available { get; set; }
        public string AvailableDate { get; set; }
        public string Role { get; set; }
        public List<SkillsDto>? skills { get; set; }

    }
}
