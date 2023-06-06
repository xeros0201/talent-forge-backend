using AutoMapper;
using TFBackend.Entities.Dto.Department;
using TFBackend.Entities.Dto.Location;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Entities.Dto.Role;
using TFBackend.Models;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Entities.Dto.StaffSkills;
using TFBackend.Entities.Dto.ProjectSkills;
using TFBackend.Entities.Dto.CalendarProjectStaff;

namespace TFBackend.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Staff, StaffDto>()
                  .ForMember(dest => dest.ProjectStaff, opt => opt.MapFrom(src => src.ProjectStaff.Select(ps => ps.Project).ToList()))
                  .ForMember(dest => dest.StaffSkills, opt => opt.MapFrom(src => src.StaffSkills.Select(ps => ps.Skill).ToList()));
            CreateMap<Department, DepartmentDto>();
            CreateMap<Staff, StaffCalendarDto>();
            CreateMap<Staff, StaffOnlyCalendarDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Skill,SkillsDto>();
            CreateMap<StaffSkills, SkillsDto>();
            CreateMap<ProjectStaff, BBProjectDto>();
            CreateMap<ProjectStaff, BBProjectNoCalendar>();
            CreateMap<Role, RolesDto>();
            CreateMap<BBProject, BBProjectDto>()
                .ForMember(dest => dest.ProjectSkill, opt => opt.MapFrom(src => src.ProjectSkill.Select(ps => ps.Skill).ToList()))
                .ForMember(dest => dest.ProjectStaff, opt => opt.MapFrom(src => src.ProjectStaff.Select(ps => ps.Staff).ToList()));
            CreateMap<BBProject, BBProjectCalendarDto>();
            CreateMap<BBProject, BBProjectNoCalendar>();
            CreateMap<CalendarProjectStaff, CalendarProjectDto>();
            CreateMap<CalendarProjectStaff, CalendarNoStaff>();
            CreateMap<Client, ClientWithProjectDto>();
            CreateMap<ProjectSkill, SkillsDto>();
            CreateMap<ProjectStaff, StaffDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<CalendarProjectStaff, CalendarProjectStaffDto>();
        }
    }
}
