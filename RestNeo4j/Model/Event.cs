using System;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        // Relationships
        public Location Location { get; set; }
    }
}
