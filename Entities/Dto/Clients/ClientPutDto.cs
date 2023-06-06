namespace TFBackend.Entities.Dto.Clients
{
    public class ClientPutDto
    {
        public string Name { get; set; }
        public string Picture { get; set; }

        public string ClientSince { get; set; }

        public int StreetNo { get; set; }

        public string StreetName { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }
        public string Active { get; set; }
        public string? LastUpdated { get; set; }


    }
}
