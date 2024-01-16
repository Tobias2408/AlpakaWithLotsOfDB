using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using RestAlpaka.Model;

namespace Neoflix.Services
{
    public class CustomerService
    {
        private readonly IDriver _driver;
        private readonly UserService _userService;
        private readonly LocationService _locationService;

        public CustomerService(IDriver driver, UserService userService, LocationService locationService)
        {
            _driver = driver;
            _userService = userService;
            _locationService = locationService;
        }

        // Create a new Customer
        public async Task<Customer> CreateAsync(Customer customer)
{
    Users user = null;
    Location location = null;
    
    if (customer.User != null)
    {
        user = await _userService.GetByIdAsync(customer.User.UserId);
        if (user == null)
        {
            user = await _userService.CreateAsync(customer.User);
        }
    }

    if (customer.Location != null)
    {
        location = await _locationService.GetByIdAsync(customer.Location.LocationId);
        if (location == null)
        {
            location = await _locationService.CreateAsync(customer.Location);
        }
    }

    var query = @"
    CREATE (c:Customer {CustomerId: $customerId, FirstName: $firstName, LastName: $lastName, PhoneNumber: $phoneNumber, Address: $address})
    WITH c
    OPTIONAL MATCH (u:User {UserId: $userId})
    OPTIONAL MATCH (l:Location {LocationId: $locationId})
    FOREACH (ignoreMe IN CASE WHEN u IS NOT NULL THEN [1] ELSE [] END |
        MERGE (c)-[:HAS_USER]->(u))
    FOREACH (ignoreMe IN CASE WHEN l IS NOT NULL THEN [1] ELSE [] END |
        MERGE (c)-[:LOCATED_AT]->(l))
    RETURN c";
    
    var parameters = new { 
        customerId = customer.CustomerId, 
        firstName = customer.FirstName, 
        lastName = customer.LastName, 
        phoneNumber = customer.PhoneNumber, 
        address = customer.Address,
        userId = user?.UserId, // Using the null-conditional operator
        locationId = location?.LocationId // Using the null-conditional operator
    };

    using (var session = _driver.AsyncSession())
    {
        var result = await session.RunAsync(query, parameters);
        var record = await result.SingleAsync();
        if (record == null)
        {
            throw new InvalidOperationException("Customer creation failed, no record returned.");
        }

        var node = record[0].As<INode>();
        return new Customer
        {
            CustomerId = node.Properties["CustomerId"].As<int>(),
            FirstName = node.Properties["FirstName"].As<string>(),
            LastName = node.Properties["LastName"].As<string>(),
            PhoneNumber = node.Properties["PhoneNumber"].As<string>(),
            Address = node.Properties["Address"].As<string>(),
            User = user,
            Location = location
        };
    }
}

public async Task<Customer> UpdateAsync(Customer customer)
{
    // Update or Create User
    Users user = null;
    if (customer.User != null)
    {
        user = await _userService.GetByIdAsync(customer.User.UserId);
        user = user != null ? await _userService.UpdateAsync(customer.User) : await _userService.CreateAsync(customer.User);
    }

    // Update or Create Location
    Location location = null;
    if (customer.Location != null)
    {
        location = await _locationService.GetByIdAsync(customer.Location.LocationId);
        location = location != null ? await _locationService.UpdateAsync(customer.Location) : await _locationService.CreateAsync(customer.Location);
    }

    // Update Customer and its relationships
    var query = @"
    MATCH (c:Customer {CustomerId: $customerId})
    SET c.FirstName = $firstName, c.LastName = $lastName, c.PhoneNumber = $phoneNumber, c.Address = $address
    WITH c
    // Delete relationships with oldUser
    OPTIONAL MATCH (c)-[r1:HAS_USER]->(oldUser)
    DELETE r1
    WITH c
    // Delete relationships with oldLocation
    OPTIONAL MATCH (c)-[r2:LOCATED_AT]->(oldLocation)
    DELETE r2
    WITH c
    // Delete oldUser node
    OPTIONAL MATCH (oldUser)
    DELETE oldUser
    WITH c
    // Delete oldLocation node
    OPTIONAL MATCH (oldLocation)
    DELETE oldLocation
    WITH c
    // Create new relationships
    OPTIONAL MATCH (u:User {UserId: $userId})
    OPTIONAL MATCH (l:Location {LocationId: $locationId})
    FOREACH (_ IN CASE WHEN u IS NOT NULL THEN [1] ELSE [] END |
        MERGE (c)-[:HAS_USER]->(u))
    FOREACH (_ IN CASE WHEN l IS NOT NULL THEN [1] ELSE [] END |
        MERGE (c)-[:LOCATED_AT]->(l))
    RETURN c, u, l";

    var parameters = new
    {
        customerId = customer.CustomerId,
        firstName = customer.FirstName,
        lastName = customer.LastName,
        phoneNumber = customer.PhoneNumber,
        address = customer.Address,
        userId = user?.UserId ?? 0, // Provide a default value if user is null
        locationId = location?.LocationId ?? 0 // Provide a default value if location is null
    };

    using (var session = _driver.AsyncSession())
    {
        var result = await session.RunAsync(query, parameters);
        var record = await result.SingleAsync();
        if (record == null)
        {
            throw new InvalidOperationException("Customer update failed, no record returned.");
        }

        // Assuming the record includes the updated Customer, User, and Location nodes
        var node = record[0].As<INode>();
        return new Customer
        {
            CustomerId = node.Properties["CustomerId"].As<int>(),
            FirstName = node.Properties["FirstName"].As<string>(),
            LastName = node.Properties["LastName"].As<string>(),
            PhoneNumber = node.Properties["PhoneNumber"].As<string>(),
            Address = node.Properties["Address"].As<string>(),
            User = user, // Updated user
            Location = location // Updated location
        };
    }
}






public async Task<IEnumerable<Customer>> GetAllAsync()
{
    var query = @"
    MATCH (c:Customer)
    OPTIONAL MATCH (c)-[:HAS_USER]->(u:User)
    OPTIONAL MATCH (c)-[:LOCATED_AT]->(l:Location)
    RETURN c, u, l";

    using (var session = _driver.AsyncSession())
    {
        var result = await session.RunAsync(query);
        var records = await result.ToListAsync();
        var customers = new List<Customer>();

        foreach (var record in records)
        {
            var customerNode = record["c"].As<INode>();
            INode userNode = null;
            INode locationNode = null;

            try
            {
                userNode = record["u"].As<INode>();
            }
            catch (KeyNotFoundException) { /* userNode remains null if "u" is not found */ }

            try
            {
                locationNode = record["l"].As<INode>();
            }
            catch (KeyNotFoundException) { /* locationNode remains null if "l" is not found */ }

            var customer = new Customer
            {
                CustomerId = customerNode.Properties["CustomerId"].As<int>(),
                FirstName = customerNode.Properties["FirstName"].As<string>(),
                LastName = customerNode.Properties["LastName"].As<string>(),
                PhoneNumber = customerNode.Properties["PhoneNumber"].As<string>(),
                Address = customerNode.Properties["Address"].As<string>(),
                User = userNode != null ? new Users
                {
                    UserId = userNode.Properties["UserId"].As<int>(),
                    Username = userNode.Properties["Username"].As<string>(),
                    Password = userNode.Properties["Password"].As<string>(),
                    Email = userNode.Properties["Email"].As<string>(),
                    Role = userNode.Properties["Role"].As<string>()
                } : null,
                Location = locationNode != null ? new Location
                {
                    LocationId = locationNode.Properties["LocationId"].As<int>(),
                    City = locationNode.Properties["City"].As<string>(),
                    Address = locationNode.Properties["Address"].As<string>(),
                    Postalcode = locationNode.Properties["Postalcode"].As<string>()
                } : null
            };

            customers.Add(customer);
        }

        return customers;
    }
}






        // Get a customer by ID

public async Task<Customer> GetByIdAsync(int customerId)
{
    var query = @"
    MATCH (c:Customer {CustomerId: $customerId})
    OPTIONAL MATCH (c)-[:HAS_USER]->(u:User)
    OPTIONAL MATCH (c)-[:LOCATED_AT]->(l:Location)
    RETURN c, u, l";

    using (var session = _driver.AsyncSession())
    {
        var result = await session.RunAsync(query, new { customerId });
        if (await result.FetchAsync())
        {
            var record = result.Current;

            var customerNode = record["c"].As<INode>();
            INode userNode = null;
            INode locationNode = null;

            try
            {
                userNode = record["u"].As<INode>();
            }
            catch (KeyNotFoundException) { /* userNode remains null if "u" is not found */ }

            try
            {
                locationNode = record["l"].As<INode>();
            }
            catch (KeyNotFoundException) { /* locationNode remains null if "l" is not found */ }

            var customer = new Customer
            {
                CustomerId = customerNode.Properties["CustomerId"].As<int>(),
                FirstName = customerNode.Properties["FirstName"].As<string>(),
                LastName = customerNode.Properties["LastName"].As<string>(),
                PhoneNumber = customerNode.Properties["PhoneNumber"].As<string>(),
                Address = customerNode.Properties["Address"].As<string>(),
                User = userNode != null ? new Users
                {
                    UserId = userNode.Properties["UserId"].As<int>(),
                    Username = userNode.Properties["Username"].As<string>(),
                    Password = userNode.Properties["Password"].As<string>(),
                    Email = userNode.Properties["Email"].As<string>(),
                    Role = userNode.Properties["Role"].As<string>()
                } : null,
                Location = locationNode != null ? new Location
                {
                    LocationId = locationNode.Properties["LocationId"].As<int>(),
                    City = locationNode.Properties["City"].As<string>(),
                    Address = locationNode.Properties["Address"].As<string>(),
                    Postalcode = locationNode.Properties["Postalcode"].As<string>()
                } : null
            };

            return customer;
        }
        return null; // Return null if no customer is found
    }
}




        // Update a customer
      

        // Delete a customer by ID
        public async Task<bool> DeleteAsync(int customerId)
        {
            var query = "MATCH (c:Customer {CustomerId: $customerId}) DELETE c";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, new { customerId });
                var summary = await result.ConsumeAsync();
                return summary.Counters.NodesDeleted > 0;
            }
        }
    }
}
