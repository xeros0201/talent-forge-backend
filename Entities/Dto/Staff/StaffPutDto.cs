namespace TFBackend.Entities.Dto.Staff
{
    public class StaffPutDto
    {
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public int? RoleId { get; set; }
        public string? Available { get; set; }
        public string? AvailableDate { get; set; }
        public List<int>? SkillIds { get; set; }
    }
}
