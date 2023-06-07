namespace TFBackend.Models
{
    public class StaffCert
    {
        public int StaffId { get; set; }

        public Staff Staff { get; set; }
        public int CertId { get; set; }
            
        public Cert Cert { get; set; }
  

        public string InterNationalId { get; set; }

        public DateTime ExpiredDate { get; set; }

        public DateTime AcquiredDate { get; set; }

        public DateTime RenewalDate { get; set; }

        public string CertLink { get; set; }    

        public string IssuingOrganisation {get; set; }

    }
}
