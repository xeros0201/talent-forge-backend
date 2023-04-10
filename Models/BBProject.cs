using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFBackend.Models
{
    public class BBProject
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description{get;set;}
        public string StartDate{get;set;}
        public string EndDate { get; set; }
        public string Active { get; set; }

        //one to many relationship
        public int? DepartmentId { get;set;}
        public Department? Department { get; set; }

        public int? LocationId { get;set;}
        public Location? Location{get;set;}

        public int? ClientId { get;set;}
        public Client? client { get;set;}
        
        
        //many to many relationship - Skills and Staff
        public ICollection<ProjectSkill> ProjectSkill { get;set;}
        public ICollection<ProjectStaff> ProjectStaff { get;set;}


    }
}