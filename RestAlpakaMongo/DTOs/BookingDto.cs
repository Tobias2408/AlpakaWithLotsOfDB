using RestAlpakaMongo.Models;

namespace RestAlpakaMongo.DTOs;

public class BookingDto
{
    public CustomerDto CustomerDto { get; set; }
    public List<AlpakaDto> AlpakaDtos { get; set; }
    public DateTime Booking_date { get; set; }
    public DateTime Start_date { get; set; }
    public DateTime End_date { get; set; }

    // You can include other relevant fields as needed
}