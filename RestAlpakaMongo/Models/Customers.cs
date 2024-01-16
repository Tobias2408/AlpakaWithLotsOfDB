using RestAlpakaMongo.GenericBase;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpakaMongo.Models
{
    public class Customers : BaseEntity
    {
       
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Phone_number { get; set; }
        public string Address { get; set; }
        public Users User { get; set; }
        public Location Location { get; set; }
    }
}
