using AutoMapper;
using TFBackend.Entities.Dto.Department;
using TFBackend.Entities.Dto.Location;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Entities.Dto.Roll;
using TFBackend.Models;

namespace TFBackend.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Staff, StaffDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Skill,SkillsDto>();
            CreateMap<Roll, RollsDto>();
        }
    }
}
