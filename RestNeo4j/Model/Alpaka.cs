using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace RestAlpaka.Model
{
    public class Alpaka
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Color: {Color}, Age: {Age}, Description: {Description}";
        }
    }
}