using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Tickets
    {
        
        public int TicketNumber { get; set; }

      

        [Range(0, 9999999.99, ErrorMessage = "Price must be a non-negative decimal with up to 2 decimal places.")]
        public decimal Price { get; set; }

        // Realtionship
        public Event Event { get; set; }
        public Customer Customer { get; set; }
    }
}
