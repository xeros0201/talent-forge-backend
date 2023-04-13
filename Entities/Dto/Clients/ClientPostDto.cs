namespace TFBackend.Entities.Dto.Clients
{
    public class ClientPostDto
    { 
    
        public string Name { get; set; }
        public string Active { get; set; }
        public string LastUpdated { get; set; }

        public List<int>? StaffIds { get; set; }

    }
}
