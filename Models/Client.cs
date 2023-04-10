using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFBackend.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name {get;set;}
        public string Active {get;set;}
        public string? LastUpdated{get;set;}
        public string? TotalProjects{get;set;}

        //one to many relationship
        public ICollection<BBProject>? Projects { get;set;}

        //many to many relationship
        public ICollection<StaffClient> StaffClients { get;set;}




    }
}