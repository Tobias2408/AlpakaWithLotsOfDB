using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestAlpakaMongo.GenericBase;

namespace RestAlpakaMongo.Models
{
    public class Reviews : BaseEntity
    {
        
     
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Review_date { get; set; }

        // Navigation properties
        public Customers Customer { get; set; }
        public Alpaka Alpaka { get; set; }
    }
}
