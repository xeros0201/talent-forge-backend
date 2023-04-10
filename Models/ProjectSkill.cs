using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TFBackend.Models
{
    public class ProjectSkill
    {
        //[Key]

        public int ProjectId { get; set; }
        public BBProject Project { get; set; }
        
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
