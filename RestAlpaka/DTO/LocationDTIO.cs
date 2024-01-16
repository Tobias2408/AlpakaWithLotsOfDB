using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.DTO
{
    public class LocationDTIO
    {
        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Postalcode { get; set; }
    }
}
