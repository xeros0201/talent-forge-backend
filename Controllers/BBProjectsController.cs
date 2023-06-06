
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Interfaces;
using TFBackend.Models;
using TFBackend.Repository;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BBProjectsController : BaseController
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public BBProjectsController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BBProject>))]
        public IActionResult GetBBProjects()
        {
            var projects = _mapper.Map<List<BBProjectDto>>(_projectRepository.GetBBProjects());
            if (!ModelState.IsValid)
                return CustomResult(ModelState, System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success",projects);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BBProject))]
        [ProducesResponseType(400)]
        public IActionResult GetBBProject(int id)
        {
            if (!_projectRepository.BBProjectExist(id)) 
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);


            var project = _mapper.Map<BBProjectDto>(_projectRepository.GetBBProject(id));
            if (!ModelState.IsValid)
                return CustomResult(ModelState, System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success", project);
        }

        [HttpGet("agendar/{id}")]
        [ProducesResponseType(200, Type = typeof(BBProject))]
        [ProducesResponseType(400)]
        public IActionResult GetBBProjectWitchCalender(int id)
        {
            if (!_projectRepository.BBProjectExist(id))
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);


            var project = _mapper.Map<BBProjectCalendarDto>(_projectRepository.GetBBProjectWithAgenda(id));
            if (!ModelState.IsValid)
                return CustomResult(ModelState, System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success", project);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BBProject))]
        public IActionResult CreateBBProject(BBProjectPostDto projectDto)
        {
            if(projectDto == null)
            {
                return CustomResult("Bad request",System.Net.HttpStatusCode.BadRequest);
            }
            var skillsCheck = _projectRepository.CheckProjectSkills(projectDto.SkillIds);
            var staffCheck = _projectRepository.CheckProjectStaffs(projectDto.StaffIds);
            if (!skillsCheck)
                return CustomResult("One of the Skill Id does not exist", System.Net.HttpStatusCode.BadRequest);
            if(!staffCheck)
                return CustomResult("One of the Staff Id does not exist", System.Net.HttpStatusCode.BadRequest);
            BBProject bBProject;
            var createProject = _projectRepository.CreateProject(projectDto,out bBProject);
            if (!createProject)
                return CustomResult("Create project failed", System.Net.HttpStatusCode.BadRequest);
            
            return CustomResult("Success",  _mapper.Map<BBProjectDto>(bBProject));

        }
        [HttpPut]
        public IActionResult UpdateProject( int id ,BBProjectPutDto projectUpdateDto)
        {
            if (!_projectRepository.BBProjectExist(id)){
                return CustomResult("No found", System.Net.HttpStatusCode.NotFound);
            }

            string err;
  
                
            if (!_projectRepository.UpdateProject(id, projectUpdateDto, out err))
            {
                return CustomResult(err, System.Net.HttpStatusCode.BadRequest);
            }

            return CustomResult(System.Net.HttpStatusCode.NoContent);
                
        }

        [HttpDelete]
        public IActionResult DeleteProject( int id )
        {
            if(id == null)
            {
                return CustomResult("No id !", System.Net.HttpStatusCode.BadRequest);

            }

            var project = _projectRepository.GetBBProject(id);
            if(project == null)
            {
                return CustomResult("Project not found !", System.Net.HttpStatusCode.BadRequest);
            }

            if (!_projectRepository.DeleteProject(project))
            {
                return CustomResult("Delete failed something went wrong !");
            }
            return CustomResult(System.Net.HttpStatusCode.NoContent);
        }
    }
}
