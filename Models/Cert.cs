namespace TFBackend.Models
{
    public class Cert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CertCategoryId { get; set; }

        public CertCategory CertCategory { get; set; }
        public ICollection<StaffCert>? Staffs { get; set; }
    }
}
