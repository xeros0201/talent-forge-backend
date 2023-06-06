using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Interfaces;
using TFBackend.Models;

namespace TFBackend.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool BBProjectExist(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }

        public bool CreateProject(BBProjectPostDto projectDto,out BBProject projectRe)
        {
            try
            {
                var project = new BBProject()
                {
                    Name = projectDto.Name,
                    Description = projectDto.Description,
                    ClientId = projectDto.ClientId,
                    StartDate = projectDto.StartDate,
                    DepartmentId = projectDto.DepartmentId,
                    LocationId = projectDto.LocationId,
                    EndDate = projectDto.EndDate,
                    Active = projectDto.Active,
                    Color  = projectDto.Color
                };
                var result = _context.Projects.Add(project);
                Save();
                AddProjectSkills(projectDto.SkillIds, project);
                AddProjectStaffs(projectDto.StaffIds, project);
                Save();
                projectRe = GetBBProject(project.Id);
                return true ;
            }
            catch (Exception e)
            {
                projectRe = null;
                return false;
            }
        }

        public BBProject GetBBProject(int id)
        {
           
            return _context.Projects
                .Include(p => p.Department)
                .Include(p => p.client)
                .Include(p => p.Location)
                .Include(p => p.ProjectSkill).ThenInclude(sk => sk.Skill)
                .Include(p => p.ProjectStaff).ThenInclude(st => st.Staff).ThenInclude(sx => sx.Role)
                .Include(p => p.ProjectStaff).ThenInclude(st => st.Staff).ThenInclude(st => st.StaffSkills).ThenInclude(s => s.Skill)
                .Where(p => p.Id == id).FirstOrDefault();

        }
        public BBProject GetBBProjectWithAgenda(int id)
        {


            return _context.Projects
            .Include(p => p.Department)
            .Include(p => p.client)
            .Include( p => p.CalendarProjectStaff).ThenInclude( pp => pp.Staff)
            .Include(p => p.Location)
            .Include(p => p.ProjectSkill).ThenInclude(sk => sk.Skill)
            .Where(p => p.Id == id )
            .FirstOrDefault();
        
 

        }

        public BBProject GetBBProjectByName(string name)
        {
            return _context.Projects.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<BBProject> GetBBProjects()
        {
            return _context.Projects
                .Include(p => p.Department)
                .Include(p => p.client)
                .Include(p => p.Location)
                .Include(p => p.ProjectSkill).ThenInclude(sk => sk.Skill)
                 .Include(p => p.ProjectStaff).ThenInclude(st => st.Staff).ThenInclude(sx => sx.Role)
                .Include(p => p.ProjectStaff).ThenInclude(st => st.Staff).ThenInclude(st => st.StaffSkills).ThenInclude(s => s.Skill)
                .ToList();
        }
        public bool ProjectSkillExist(int skillId)
        {
            return _context.Skills.Any(ps => ps.Id == skillId);
        }
        public bool ProjectStaffExist(int staffId)
        {
            return _context.Staff.Any(ps => ps.Id == staffId);
        }


        public bool CheckProjectSkills(List<int> skillIds)
        {

            if (skillIds == null || skillIds?.Count() < 1)
            {

                return false;
            }

            foreach (int skillId in skillIds)
            {
                if (!ProjectSkillExist(skillId))
                {
                    return false;
                }
            }

            return true;

        }

        public bool CheckProjectStaffs(List<int> staffIds)
        {


            if (staffIds == null || staffIds?.Count() < 1)
            {

                return false;
            }

            foreach (int staffId in staffIds)
            {
                if (!ProjectStaffExist(staffId))
                {

                    return false;
                }

            }
            return true;
        }

        public void AddProjectSkills(List<int> skillIds, BBProject project)
        {


            foreach (int skillId in skillIds)
            {

                _context.ProjectSkills.Add(new ProjectSkill()
                {
                    ProjectId = project.Id,
                    SkillId = skillId,
                    Project = project,
                    Skill = _context.Skills.Where(s => s.Id == skillId).FirstOrDefault(),
                });
            }

        }

        public void AddProjectStaffs(List<int> staffIds, BBProject project)
        {
            foreach (int staffId in staffIds)
            {

                _context.ProjectStaff.Add(new ProjectStaff()
                {
                    ProjectId = project.Id,
                    StaffId = staffId,
                    Project = project,
                    Staff = _context.Staff.Where(s => s.Id == staffId).FirstOrDefault(),
                });
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProject(int id, BBProjectPutDto projectDto, out string errorMessage)
        {

            
            try
            {   

                var project = GetBBProject(id);
                project.Color = projectDto.Color ?? project.Color;
                if (projectDto.Name != "")
                {
                    project.Name = projectDto.Name;
                }
                if (projectDto.Description != "")
                {
                    project.Description = projectDto.Description;
                }
                if (_context.Client.Any(c => c.Id == projectDto.ClientId))
                {
                    project.ClientId = projectDto.ClientId;

                }
                else
                {
                    throw new Exception("Client not found !");
                }
                if (projectDto.StartDate != "")
                {
                    project.StartDate = projectDto.StartDate;
                }
                if (_context.Departments.Any(d => d.Id == projectDto.DepartmentId))
                {
                    project.DepartmentId = projectDto.DepartmentId;
                }
                else
                {
                    throw new Exception("Department not found !");
                }
                if (_context.Locations.Any(l => l.Id == projectDto.LocationId))
                {
                    project.LocationId = projectDto.LocationId;
                }
                else
                {
                    throw new Exception("Location not found !");
                }
                if (projectDto.EndDate != "")
                {
                    project.EndDate = projectDto.EndDate;
                }
                if (projectDto.Active != "")
                {
                    project.Active = projectDto.Active;
                }

                if (projectDto.SkillsIds != null || projectDto.SkillsIds.Count() > 0)
                {
                    var projectskills = _context.ProjectSkills.Where(ps => ps.ProjectId == id).ToList();
                    foreach (var projectskill in projectskills)
                    {
                        _context.ProjectSkills.Remove(projectskill);
                    }

                    foreach (int skillId in projectDto.SkillsIds)
                    {

                        if (!ProjectSkillExist(skillId))
                        {
                            throw new Exception("One of Skill Id not found !");
                        }                      
                            _context.ProjectSkills.Add(new ProjectSkill()
                            {
                                ProjectId = project.Id,
                                SkillId = skillId,
                                Project = project,
                                Skill = _context.Skills.Where(s => s.Id == skillId).FirstOrDefault(),
                            });              
                    }
                }

                if (projectDto.StaffIds != null || projectDto.StaffIds.Count() > 0)
                {
                    var projectstaffs = _context.ProjectStaff.Where(ps => ps.ProjectId == id).ToList();
                    foreach (var projectstaff in projectstaffs)
                    {
                        _context.ProjectStaff.Remove(projectstaff);
                    }

                    foreach (int staffId in projectDto.StaffIds)
                    {

                        if (!ProjectStaffExist(staffId))
                        {
                            throw new Exception("One of Staff Id not found !");
                        }
                        _context.ProjectStaff.Add(new ProjectStaff()
                        {
                            ProjectId = project.Id,
                            StaffId = staffId,
                            Project = project,
                            Staff = _context.Staff.Where(s => s.Id == staffId).FirstOrDefault(),
                        });
                    }
                }

                _context.Projects.Update(project);
                errorMessage = null;
                return Save();
            }
            catch (Exception e)
            {

                errorMessage = e.Message;
                return false;
            }


        
        }

        public bool DeleteProject(BBProject project)
        {

             _context.Projects.Remove(project);

            return Save();

        }
    }
}
