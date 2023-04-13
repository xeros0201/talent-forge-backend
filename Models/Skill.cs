using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFBackend.Models
{
    public class Skill { 
        public int Id { get; set; }
        public string Name {get;set;}
        public string Color {get;set;}


        //many to many relationship
        public ICollection<StaffSkills> StaffSkills { get; set; }
        public ICollection<ProjectSkill> ProjectSkill { get; set; }
        

    }
}