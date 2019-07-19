using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TimeTrackerAPI.Client.Models
{
    public class ProjectInputModel
    {
        [Required]
        public string Name { get; set; }
        public long ClientId { get; set; }


    }
}
