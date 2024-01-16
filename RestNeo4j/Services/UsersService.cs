using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using RestAlpaka.Model;

namespace Neoflix.Services
{
    public class UserService
    {
        private readonly IDriver _driver;

        public UserService(IDriver driver)
        {
            _driver = driver;
        }
        // Update an existing User
        public async Task<Users> UpdateAsync(Users user)
        {
            var query = @"
                MATCH (u:User {UserId: $userId})
                SET u.Username = $username, 
                    u.Password = $password, 
                    u.Email = $email, 
                    u.Role = $role
                RETURN u.UserId AS UserId, u.Username AS Username, u.Password AS Password, u.Email AS Email, u.Role AS Role";

            var parameters = new
            {
                userId = user.UserId,
                username = user.Username,
                password = user.Password,
                email = user.Email,
                role = user.Role
            };

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, parameters);
                var record = await result.SingleAsync();

                if (record == null)
                {
                    throw new InvalidOperationException("User update failed, no record returned.");
                }

                var updatedUser = new Users
                {
                    UserId = record["UserId"].As<int>(),
                    Username = record["Username"].As<string>(),
                    Password = record["Password"].As<string>(),
                    Email = record["Email"].As<string>(),
                    Role = record["Role"].As<string>()
                };

                return updatedUser;
            }
        }
        public async Task<Users> CreateAsync(Users user)
        {
            // Use GetByIdAsync to check if a User with the given UserId already exists
            var existingUser = await GetByIdAsync(user.UserId);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with the given UserId already exists.");
            }

            // Proceed to create a new user if no existing user was found
            return await CreateUserInDatabase(user);
        }

        private async Task<Users> CreateUserInDatabase(Users user)
        {
            var createQuery = @"
    CREATE (u:User {
        UserId: $userId, 
        Username: $username, 
        Password: $password, 
        Email: $email, 
        Role: $role
    })";

            using (var session = _driver.AsyncSession())
            {
                // Execute the create query without expecting a return
                await session.RunAsync(createQuery, new { userId = user.UserId, username = user.Username, password = user.Password, email = user.Email, role = user.Role });

                // Optionally, retrieve the created user to confirm creation
                var retrieveQuery = "MATCH (u:User {UserId: $userId}) RETURN u.UserId AS UserId, u.Username AS Username, u.Password AS Password, u.Email AS Email, u.Role AS Role";
                var retrieveResult = await session.RunAsync(retrieveQuery, new { userId = user.UserId });
        
                if (await retrieveResult.FetchAsync()) // FetchAsync returns true if there's a record
                {
                    var record = retrieveResult.Current;

                    return new Users
                    {
                        UserId = record["UserId"].As<int>(),
                        Username = record["Username"].As<string>(),
                        Password = record["Password"].As<string>(),
                        Email = record["Email"].As<string>(),
                        Role = record["Role"].As<string>()
                    };
                }
                else
                {
                    // Handle the case where no user is found after creation
                    throw new InvalidOperationException("User creation failed, user not found after creation.");
                }
            }
        }








        // Get all users
        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            var query = "MATCH (u:User) RETURN u.UserId AS UserId, u.Username AS Username, u.Password AS Password, u.Email AS Email, u.Role AS Role";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query);
                var users = await result.ToListAsync(record => new Users
                {
                    UserId = record["UserId"].As<int>(),
                    Username = record["Username"].As<string>(),
                    Password = record["Password"].As<string>(),
                    Email = record["Email"].As<string>(),
                    Role = record["Role"].As<string>()
                });
                return users;
            }
        }

        // Get a user by ID
        public async Task<Users> GetByIdAsync(int userId)
        {
            var query = "MATCH (u:User {UserId: $userId}) RETURN u.UserId AS UserId, u.Username AS Username, u.Password AS Password, u.Email AS Email, u.Role AS Role";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, new { userId });
                if (await result.FetchAsync()) // FetchAsync returns true if there's a record
                {
                    var record = result.Current;

                    return new Users
                    {
                        UserId = record["UserId"].As<int>(),
                        Username = record["Username"].As<string>(),
                        Password = record["Password"].As<string>(),
                        Email = record["Email"].As<string>(),
                        Role = record["Role"].As<string>()
                    };
                }

                return null; // Return null if no record is found
            }
        }


        // Delete a user by ID
        public async Task<bool> DeleteAsync(int userId)
        {
            var query = "MATCH (u:User {UserId: $userId}) DELETE u";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, new { userId });
                var summary = await result.ConsumeAsync();
                return summary.Counters.NodesDeleted > 0;
            }
        }
    }
}
