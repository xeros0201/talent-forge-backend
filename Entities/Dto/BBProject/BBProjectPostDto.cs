using TFBackend.Models;

namespace TFBackend.Entities.Dto.BBProject
{
    public class BBProjectPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ClientId { get; set; }

        public string StartDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public string EndDate { get; set; }
        public string Active { get; set; }
        public string? Color { get; set; }
        public List<int>? SkillIds { get; set; }
        public List<int>? StaffIds { get; set; }
    }
}
