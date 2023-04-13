using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TFBackend.Models
{
    public class StaffClient
    {
        //[Key]
        public int StaffId { get; set; }
        public int ClientId { get; set; }
        public Staff Staff { get; set; }
        public Client Client { get; set; }

    }
}
