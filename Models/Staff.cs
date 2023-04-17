using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFBackend.Models
{
    public class Staff
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Picture {get;set;}
        public string Available {get;set;}
        public string AvailableDate{get;set;}
        public string password { get; set; }
        //one to many realtionship
        public int? RoleId { get;set;}
        public Role? Role { get;set;}

        //many to many relationship
        public ICollection<StaffSkills>? StaffSkills { get;set;}
        public ICollection<ProjectStaff>? ProjectStaff { get;set;}
        public ICollection<StaffClient>? StaffClients { get;set;}

    }
}