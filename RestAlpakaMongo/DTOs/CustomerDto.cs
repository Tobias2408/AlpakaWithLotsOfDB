namespace RestAlpakaMongo.DTOs;

public class CustomerDto
{
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string Phone_number { get; set; }
    public string Address { get; set; }
    
    // User and Location details
    public UserDto User { get; set; }
    public LocationDto Location { get; set; }
}