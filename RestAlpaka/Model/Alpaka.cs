using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace RestAlpaka.Model
{
    public class Alpaka
    {
        [Key]
        public int alpaka { get; set; }
        public string Alpaka_name { get; set; }
        public string color { get; set; }

        public int age {  get; set; }  

        public string description { get; set; }


    }
}
