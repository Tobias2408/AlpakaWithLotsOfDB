using System;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Relationships
        public Booking Booking { get; set; }
    }
}
