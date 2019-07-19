
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerAPI.Client.Models
{
    public class ClientInputModel
    {

        [Required]
        public string Name { get; set; }

       
    }
}
