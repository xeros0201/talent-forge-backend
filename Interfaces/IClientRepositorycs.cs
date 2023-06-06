using TFBackend.Entities.Dto.BBProject;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Models;

namespace TFBackend.Interfaces
{
    public interface IClientRepositorycs
    {


        ICollection<Client> GetClients();
        Client GetClient(int id);
 
        bool CreateClient(ClientPostDto client);
        bool clientExist(int  id);

        bool UpdateClient(ClientPutDto client,int id);
        bool DeleteClient(Client client);
            
        bool Save();
    }
}
