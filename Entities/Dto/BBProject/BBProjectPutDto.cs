using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;

namespace TFBackend.Entities.Dto.BBProject
{
    public class BBProjectPutDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public string StartDate { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public string EndDate { get; set; }
        public string Active { get; set; }
        public List<int>? SkillsIds { get; set; }
        public List<int>? StaffIds { get; set; }
    }
}
