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
        public string  Username { get;set;}
        public string? Picture {get;set;}
        public string Available {get;set;}
        public string AvailableDate{get;set;}
        public string Password { get; set; }
        //one to many realtionship
        public int? RoleId { get;set;}
        public Role? Role { get;set;}

        //many to many relationship  b
        public ICollection<StaffSkills>? StaffSkills { get;set;}
        public ICollection<ProjectStaff>? ProjectStaff { get;set;}

        public ICollection<CalendarProjectStaff>? CalendarProjectStaff { get; set; }

        public ICollection<StaffCert>? StaffCerts { get;set;}
        public ICollection<BBProject>? Projects { get; set; }
    }
}