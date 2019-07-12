using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Domain;

namespace TimeTracker.Models
{
    public class TimeEntryModel
    {

        public long Id { get; set; }

        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ClientName { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set;  }

        public DateTime EntryDate { get; set;  }

        public int Hours { get; set; }

        public string Description { get; set; }

        public decimal HourRate { get; set; }

        public decimal Total => Hours * HourRate; 



        public static TimeEntryModel FromTimeEntry(TimeEntry timeEnrty)
        {

            return new TimeEntryModel
            {
                Id = timeEnrty.Id,
                ProjectId = timeEnrty.Project.Id,
                ProjectName = timeEnrty.Project.Name,
                ClientName = timeEnrty.Project.Client.Name,
                UserId = timeEnrty.User.Id,
                UserName = timeEnrty.User.Name,
                EntryDate = timeEnrty.EntryDate,
                Hours = timeEnrty.Hours,
                Description = timeEnrty.Description,
                HourRate = timeEnrty.HourRate

            };
            

        }
    }
}
