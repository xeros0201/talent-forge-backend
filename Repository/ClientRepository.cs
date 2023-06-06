using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Interfaces;
using TFBackend.Models;

namespace TFBackend.Repository
{
    public class ClientRepository : IClientRepositorycs
    {

        private readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateClient(ClientPostDto client)
        {
            try
            {
                var newClient = new Client()
                {
                    Active = client.Active,
                    ClientSince = client.ClientSince,
                    LastUpdated = DateTime.Now.ToString(),
                    Name = client.Name,
                    Picture = client.Picture,
                    State = client.State,
                    StreetName = client.StreetName,
                    StreetNo = client.StreetNo,
                    Suburb = client.Suburb,
                };

                var result = _context.Client.Add(newClient);

                return Save();

            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public Client GetClient(int id)
        {
        
                return _context.Client
              .Include(cl => cl.Projects)
                .ThenInclude(p => p.ProjectStaff)
                    .ThenInclude(st => st.Staff).Include(p => p.Projects)
                .Include(st => st.Projects)
                    .ThenInclude(ps => ps.ProjectSkill)
                     .ThenInclude(sk => sk.Skill).Where( cl => cl.Id == id).FirstOrDefault();
         
            
        }

        public ICollection<Client> GetClients()
        {
            return _context.Client
                .Include( cl => cl.Projects )
                    .ThenInclude(p => p.ProjectStaff)
                        .ThenInclude(st => st.Staff).Include(p => p.Projects)
                .Include(st => st.Projects)
                    .ThenInclude(ps => ps.ProjectSkill)
                        .ThenInclude( sk => sk.Skill).ToList();
        }


        public bool clientExist(int id)
        {
            return _context.Client.Any( c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClient(ClientPutDto client, int id)
        {
            var clientToUpdate = _context.Client.Where(c => c.Id == id).FirstOrDefault();
            if (client.Suburb != null)
            {
                clientToUpdate.Suburb = client.Suburb;
            } 
            if(client.StreetNo!= null)
            {
                clientToUpdate.StreetNo = client.StreetNo;
            }
            if(client.State != null)
            {
                clientToUpdate.State = client.State;
            }
            if(client.StreetName!= null)
            {
                clientToUpdate.StreetName = client.StreetName; 
            }
            if(client.Name!= null)
            {
                clientToUpdate.Name =client.Name;   
            }
            if(client.Active != null)
            {
                clientToUpdate.Active = client.Active;
            }
            if(client.ClientSince != null) {
                clientToUpdate.ClientSince = client.ClientSince;
            }
            client.LastUpdated = DateTime.Now.ToString();
            return Save();
            
        }

        public bool DeleteClient(Client client)
        {
            _context.Client.Remove(client);

            return Save();
        }
    }
}
