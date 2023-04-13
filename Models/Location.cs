using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFBackend.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name {get; set;}

        //one-to-many relationship - Project
        public ICollection<BBProject>? Projects { get; set; }
        
    }
}