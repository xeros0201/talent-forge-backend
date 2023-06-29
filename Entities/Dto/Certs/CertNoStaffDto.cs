using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;

namespace TFBackend.Entities.Dto.Certs
{
    public class CertNoStaffDto
    {
        public int StaffId { get; set; }
 
        public int CertId { get; set; }

        public CertNoStaffList Cert { get; set; }
 

 

        public string InterNationalId { get; set; }

        public DateTime ExpiredDate { get; set; }

        public DateTime AcquiredDate { get; set; }

        public DateTime RenewalDate { get; set; }

        public string CertLink { get; set; }

        public string IssuingOrganisation { get; set; }

    }
}
