using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Payments
    {
        
        public int Payment_id { get; set; }

        public int Booking_id { get; set; }

        public DateTime Payment_date { get; set; }

        public decimal Amount { get; set; }

    }
}
