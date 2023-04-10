using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;

using TFBackend.Models;


namespace TFBackend.Entities.Dto.BBProject
{
    public class BBProjectDto
    {
        public List<SkillsDto> Skills { get; set; }
        public List<StaffDto> Staff { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Client { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string? Location { get; set; }
        public string? Department { get; set; }
        public string Active { get; set; }
        public int Id { get; set; }

    }
}
