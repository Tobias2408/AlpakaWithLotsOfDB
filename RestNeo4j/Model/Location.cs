using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAlpaka.Model
{
    public class Location
    {

      
        public int LocationId { get; set; }

       
        public string City { get; set; }

      
        public string Address { get; set; }

        public string Postalcode { get; set; }


    }
}
