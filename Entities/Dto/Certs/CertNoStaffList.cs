using TFBackend.Models;

namespace TFBackend.Entities.Dto.Certs
{
    public class CertNoStaffList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CertCategoryId { get; set; }

        public CateNoCert CertCategory { get; set; }
 
    }
}
