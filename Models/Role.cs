using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TFBackend.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Staff>? Staffs { get; set;}
    }
}
