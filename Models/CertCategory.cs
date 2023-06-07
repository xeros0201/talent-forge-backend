namespace TFBackend.Models
{
    public class CertCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Cert>? Certs { get; set; }
    }
}
