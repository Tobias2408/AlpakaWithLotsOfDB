using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.DTO
{
    public class EventDTO
    {
        public int Location_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Event_date { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Capacity must be a non-negative value.")]
        public int Capacity { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string Name { get; set; }
    }
}
