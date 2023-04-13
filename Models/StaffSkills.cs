using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TFBackend.Models
{
    public class StaffSkills
    {
        //[Key]
        public int StaffId { get; set; }
        public int SkillId { get; set; }
        public Staff Staff { get; set; }
        public Skill Skill { get; set; }

    }
}
