using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Tickets
    {
   
        public int TicketNumber { get; set; }

     
        public int Event_id { get; set; }

        
        public int Customer_id { get; set; }

      
        public decimal Price { get; set; }

   
    }
}
