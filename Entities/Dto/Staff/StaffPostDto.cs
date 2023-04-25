using TFBackend.Models;
namespace TFBackend.Entities.Dto.Staff
{
    public class StaffPostDto
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Username { get; set; }
        public int? RoleId { get; set; }
        public string Available { get; set; }
        public string AvailableDate { get; set; }
        public string Password { get; set; }
        public List<int>? SkillIds { get; set; }

    }
}
