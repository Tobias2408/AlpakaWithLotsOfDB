using MongoDB.Driver;
using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestAlpakaMongo.DTOs;

namespace RestAlpakaMongo.Services;

public class BookingService : MongoDbService<Booking>
{
    private readonly CustomersService _customersService;
    private readonly AlpakaService _alpakaService;
    

    public BookingService(IConfiguration config, CustomersService customersService, AlpakaService alpakaService)
        : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Booking")
    {
        _customersService = customersService;
        _alpakaService = alpakaService;
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await GetAllAsync();
    }

    public async Task<Booking> GetBookingByIdAsync(string id)
    {
        return await GetByIdAsync(id);
    }

    public async Task<Booking> CreateBookingAsync(BookingDto bookingDto)
    {
        // Use CustomersService to handle customer creation
        var customerDto = bookingDto.CustomerDto;
        Customers customer;

        // Try to create a new customer, or get an existing one if it already exists
        try
        {
            customer = await _customersService.CreateCustomerAsync(customerDto);
        }
        catch (Exception e) when (e.Message.Contains("A user with the same username or email already exists"))
        {
            // If the user already exists, fetch the existing customer
            customer = await _customersService.GetCustomerByUserUsernameAsync(customerDto.User.Username);

         
        }

        // Use AlpakaService to handle alpaka creation
        var alpakaDtos = bookingDto.AlpakaDtos;
        var alpakas = new List<Alpaka>();
        var alpaka = new Alpaka();

        foreach (var alpakaDto in alpakaDtos)
        {
            try
            {
                 alpaka = await _alpakaService.CreateOrUpdateAlpakaAsync(alpakaDto);
                alpakas.Add(alpaka);
            }
            catch (Exception e) 
            {
                alpaka = await _alpakaService.GetByAlpakaNameAsync(alpakaDto.AlpakaName);
            }
        }

        // Create the Booking object
        var newBooking = new Booking
        {
            Booking_date = bookingDto.Booking_date,
            Start_date = bookingDto.Start_date,
            End_date = bookingDto.End_date,
            Customer = customer,
            Alpaka = alpakas
        };

        await CreateAsync(newBooking); // Save the new booking
        return newBooking; // Return the created booking
    }


    
}