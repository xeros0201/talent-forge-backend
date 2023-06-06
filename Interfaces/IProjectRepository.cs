using TFBackend.Entities.Dto.BBProject;
using TFBackend.Models;

namespace TFBackend.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<BBProject> GetBBProjects();
        BBProject GetBBProject(int id);
        BBProject GetBBProjectWithAgenda(int id);
        BBProject GetBBProjectByName(string name);
        bool CreateProject(BBProjectPostDto project, out BBProject projectRe);
        bool BBProjectExist(int id);

        bool ProjectSkillExist(int id);

        bool ProjectStaffExist(int id);

        bool CheckProjectSkills(List<int> id);

        bool CheckProjectStaffs(List<int> id);

        void AddProjectSkills(List<int> id, BBProject project);

        void AddProjectStaffs(List<int> id, BBProject project);

        bool UpdateProject(int id, BBProjectPutDto project, out string err);

        bool DeleteProject(BBProject project);
        bool Save();
    }



}
