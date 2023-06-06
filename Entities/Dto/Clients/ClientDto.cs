using TFBackend.Entities.Dto.BBProject;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.Clients
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Picture { get; set; }

        public string ClientSince { get; set; }

        public int StreetNo { get; set; }

        public string StreetName { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }
        public string Active { get; set; }
        public string? LastUpdated { get; set; }

        public List<BBProjectDto> Projects { get; set; }


    }
}
