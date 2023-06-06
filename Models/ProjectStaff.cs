using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TFBackend.Models
{
    public class ProjectStaff
    {
        //[Key]\
 
        public int ProjectId { get; set; }

        public BBProject Project { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set;}

     

    }
}
