using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestAlpakaMongo.GenericBase;

namespace RestAlpakaMongo.Models
{
    public class Booking : BaseEntity
    {
    
     
        public DateTime Booking_date { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }

       
        public Customers Customer { get; set; }
        public List<Alpaka> Alpaka { get; set; }
    }
}
