using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Bookings
    {

        public int Booking_id { get; set; }

    
        public int Customer_id { get; set; }

   
        public int Alpaka_id { get; set; }

     
        public DateTime Booking_date { get; set; }

        
        public DateTime Start_date { get; set; }

      
        public DateTime End_date { get; set; }

        public Customers Customers { get; set; }
        public Location Location { get; set; } 

    }
}

