using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Payments
    {
        [Key]
        public int Payment_id { get; set; }

        public int Booking_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Payment_date { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "Amount must be a non-negative decimal with up to 2 decimal places.")]
        public decimal Amount { get; set; }

    }
}
