using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.Certs
{
    public class CertCateStaff
    {
        public int StaffId { get; set; }

        public StaffDto Staff { get; set; }
        public int CertId { get; set; }

        public Cert Cert { get; set; }
        public int CertCategoryId { get; set; }

        public CertCategory CertCategory { get; set; }

        public string InterNationalId { get; set; }

        public DateTime ExpiredDate { get; set; }

        public DateTime AcquiredDate { get; set; }

        public DateTime RenewalDate { get; set; }

        public string CertLink { get; set; }

        public string IssuingOrganisation { get; set; }

    }
}
