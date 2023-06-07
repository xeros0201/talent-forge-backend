namespace TFBackend.Entities.Dto.Certs
{
    public class CertDtoPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CateId { get; set; }
    }
}
