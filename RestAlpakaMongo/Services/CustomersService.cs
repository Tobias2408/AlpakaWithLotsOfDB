using RestAlpakaMongo.Models;
using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using RestAlpakaMongo.DTOs;

namespace RestAlpakaMongo.Services
{
    public class CustomersService : MongoDbService<Customers>
    {
        private readonly UserService _userService;
        private readonly LocationService _locationService;
     
        
        public CustomersService(IConfiguration config, UserService userService, LocationService locationService)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Customers")
        {
            
            _userService = userService;
            _locationService = locationService;
        }
        
        
        public async Task<Users> GetUserByUsernameAsync(string username)
        {
            return await _userService.GetByUsernameAsync(username);
        }
        public async Task<Customers> GetCustomerByUserUsernameAsync(string username)
        {
            var filter = Builders<Customers>.Filter.Eq("User.Username", username);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        // Other CRUD methods...

        // Create a customer and associated user and location
        public async Task<Customers> CreateCustomerAsync(CustomerDto customerDto)
        {
            // Check if a user with the same username or email already exists
            if (await UserExistsAsync(customerDto.User.Username, customerDto.User.Email))
            {
                throw new Exception("A user with the same username or email already exists.");
            }

            // Create User object and map properties
            var user = new Users
            {
                Username = customerDto.User.Username,
                Password = customerDto.User.Password,
                Email = customerDto.User.Email,
                Role = customerDto.User.Role
            };
            await _userService.CreateAsync(user);

            // Create Location object and map properties
            var location = new Location
            {
                City = customerDto.Location.City,
                Address = customerDto.Location.Address,
                Postalcode = customerDto.Location.Postalcode
            };

            // Check if a location with the same details already exists
            if (await LocationExistsAsync(location))
            {
                throw new Exception("A location with the same details already exists.");
            }

            await _locationService.CreateAsync(location);

            // Create Customer object with User and Location
            var customer = new Customers
            {
                First_name = customerDto.First_name,
                Last_name = customerDto.Last_name,
                Phone_number = customerDto.Phone_number,
                Address = customerDto.Address,
                User = user,
                Location = location
            };

            await CreateAsync(customer);
            return customer;
        }

        // Update a customer and associated user and location
        public async Task<Customers> UpdateCustomerAsync(string id, CustomerDto customerDto)
        {
            var customer = await GetByIdAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            // Update User object and map properties
            var user = customer.User;
            user.Username = customerDto.User.Username;
            user.Password = customerDto.User.Password;
            user.Email = customerDto.User.Email;
            user.Role = customerDto.User.Role;
            await _userService.UpdateAsync(user.Id, user);

            // Update Location object and map properties
            var location = customer.Location;
            location.City = customerDto.Location.City;
            location.Address = customerDto.Location.Address;
            location.Postalcode = customerDto.Location.Postalcode;
            await _locationService.UpdateAsync(location.Id, location);

            // Update Customer object and map properties
            customer.First_name = customerDto.First_name;
            customer.Last_name = customerDto.Last_name;
            customer.Phone_number = customerDto.Phone_number;
            customer.Address = customerDto.Address;
            await UpdateAsync(id, customer);

            return customer;
        }

        // Check if a user with the same username or email already exists
        public async Task<bool> UserExistsAsync(string username, string email)
        {
          
            return await _userService.UserExistsAsync(username,email);
        }

        // Check if a location with the same details already exists
        public async Task<bool> LocationExistsAsync(Location location)
        {
           return await  _locationService.LocationExists(location);
        }
    }
}
