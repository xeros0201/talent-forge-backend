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
        public string Picture { get;set;}

        public string ClientSince { get; set; }

        public int StreetNo { get; set; }

        public string StreetName { get; set; }
        public string Suburb  { get; set; }
        public string State  { get; set; }
        public string Active {get;set;}
        public string? LastUpdated{get;set;}
       
     
        //one to many relationship
        public ICollection<BBProject>? Projects { get;set;}

     




    }
}