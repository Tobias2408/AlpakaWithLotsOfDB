using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.DTO
{
    public class TicketsDTO
    {
        [ForeignKey("Event")]
        public int Event_id { get; set; }

        [ForeignKey("Customers")]
        public int Customer_id { get; set; }

        [Range(0, 9999999.99, ErrorMessage = "Price must be a non-negative decimal with up to 2 decimal places.")]
        public decimal Price { get; set; }

    }
}
