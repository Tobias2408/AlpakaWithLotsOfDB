using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestAlpakaMongo.GenericBase;

namespace RestAlpakaMongo.Models
{
    public class Tickets : BaseEntity
    {
     
        public int TicketNumber { get; set; }
   
        public decimal Price { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public Customers Customer { get; set; }
    }
}
