using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.Model
{
    public class Reviews
    {
      
        public int Review_id { get; set; }

       
        public int Customer_id { get; set; }

     
        public int Alpaka_id { get; set; }


        public int Rating { get; set; }

        public string Comment { get; set; }

      
        public DateTime Review_date { get; set; }

    }

   

   

}

