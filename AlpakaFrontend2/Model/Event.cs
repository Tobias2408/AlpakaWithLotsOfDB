using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Event
    {
    
        public int Event_id { get; set; }

        public int Location_id { get; set; }


        public DateTime Event_date { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
