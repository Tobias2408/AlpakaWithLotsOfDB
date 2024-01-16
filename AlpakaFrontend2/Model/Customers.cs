using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Customers
    {
       
        public int Customer_id { get; set; }

       
        public int User_id { get; set; }

       
        public int Location_id { get; set; }

       
        public string First_name { get; set; }

       
        public string Last_name { get; set; }

     
        public string Phone_number { get; set; }

     
        public string Address { get; set; }


    }
}

