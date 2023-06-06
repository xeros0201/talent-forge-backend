using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.Clients
{
    public class ClientWithProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
        public string? LastUpdated { get; set; }


    }
}
