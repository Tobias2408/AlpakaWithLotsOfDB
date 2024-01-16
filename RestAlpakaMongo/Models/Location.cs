using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RestAlpakaMongo.GenericBase;

namespace RestAlpakaMongo.Models
{
    public class Location : BaseEntity
    {

        

        public string City { get; set; }

        public string Address { get; set; }

        public string Postalcode { get; set; }
    }
}
