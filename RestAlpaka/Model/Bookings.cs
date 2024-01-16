using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Bookings
    {
        [Key]
        public int Booking_id { get; set; }

        [ForeignKey("Customer")]
        public int Customer_id { get; set; }

        [ForeignKey("Alpaka")]
        public int Alpaka_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Booking_date { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start_date { get; set; }

        [DataType(DataType.Date)]
        public DateTime End_date { get; set; }

    }
}

