using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TFBackend.Entities.Dto.CalendarProjectStaff;
using TFBackend.Entities.Dto.Clients;
using TFBackend.Entities.Dto.Department;
using TFBackend.Entities.Dto.Location;

namespace TFBackend.Models
{
    public class BBProjectCalendarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Active { get; set; }
        public string? Color { get; set; }
        //one to many relationship
        public int? DepartmentId { get; set; }

        public int? ManagerId { get; set; }

        public Staff? Manager { get; set; }
        public DepartmentDto? Department { get; set; }

        public int? LocationId { get; set; }
        public LocationDto? Location { get; set; }

        public int? ClientId { get; set; }
        public ClientWithProjectDto? client { get; set; }


        //many to many relationship - Skills and Staff

 

        public ICollection<CalendarProjectDto>? CalendarProjectStaff { get; set; }


    }
}