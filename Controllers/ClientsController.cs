using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Entities.Dto.Skills;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Interfaces;
using TFBackend.Models;
using TFBackend.Repository;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : BaseController
    {
        private readonly IClientRepositorycs _clientRepositorycs;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepositorycs clientRepositorycs, IMapper mapper)
        {
            _clientRepositorycs = clientRepositorycs;
            _mapper = mapper;
        }

        // GET: api/Clients
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Client>))] 
        public async Task<IActionResult> GetClient()
        {

            var Clients = _mapper.Map<List<ClientDto>>( _clientRepositorycs.GetClients());


            if (!ModelState.IsValid)
                return CustomResult(ModelState, System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success", Clients);
        }
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Client))]
        public IActionResult createClient(ClientPostDto clientDto)
        {
            if (clientDto == null)
            {
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            }
            
            var createProject = _clientRepositorycs.CreateClient(clientDto);
            if (!createProject)
                return CustomResult("Create project failed", System.Net.HttpStatusCode.BadRequest);

            return CustomResult("Success");

        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Client))]
        [ProducesResponseType(400)]
        public IActionResult GetBBProject(int id)
        {
            if (!_clientRepositorycs.clientExist(id))
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);


            var client = _mapper.Map<ClientDto>(_clientRepositorycs.GetClient(id));
            if (!ModelState.IsValid)
                return CustomResult(ModelState, System.Net.HttpStatusCode.BadRequest);
            return CustomResult("Success", client);
        }
        [HttpPut]
        public IActionResult UpdateProject(int id, ClientPutDto clientDto)
        {
            if (!_clientRepositorycs.clientExist(id))
            {
                return CustomResult("No found", System.Net.HttpStatusCode.NotFound);
            }
 


            if (!_clientRepositorycs.UpdateClient(clientDto,id))
            {
                return CustomResult("Something went wrong !", System.Net.HttpStatusCode.BadRequest);
            }

            return CustomResult(System.Net.HttpStatusCode.NoContent);

        }


        [HttpDelete]
        public IActionResult DeleteClient(int id)
        {
            if (id == null)
            {
                return CustomResult("No id !", System.Net.HttpStatusCode.BadRequest);

            }

            var client = _clientRepositorycs.GetClient(id);
            if (client == null)
            {
                return CustomResult("Project not found !", System.Net.HttpStatusCode.BadRequest);
            }

            if (!_clientRepositorycs.DeleteClient(client))
            {
                return CustomResult("Delete failed something went wrong !");
            }
            return CustomResult(System.Net.HttpStatusCode.NoContent);
        }
    }


}
