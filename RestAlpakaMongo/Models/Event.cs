using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestAlpakaMongo.GenericBase;

namespace RestAlpakaMongo.Models
{
    public class Event : BaseEntity
    {

        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        // Relationships
        public Location Location { get; set; }
    }
}
