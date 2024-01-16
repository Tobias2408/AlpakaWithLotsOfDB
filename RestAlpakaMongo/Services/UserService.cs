using RestAlpakaMongo.Models;
using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks; // Add this import for Task

namespace RestAlpakaMongo.Services
{
    public class UserService : MongoDbService<Users>
    {
        // Make the _collection field protected
        protected readonly IMongoCollection<Users> _collection;

        public UserService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Users")
        {
            // Assign the collection to the protected field
            _collection = base._collection;
        }

        // Add the UserExists method to check if a user already exists
        public async Task<bool> UserExistsAsync(string username, string email)
        {
            var userByUsername = await _collection.Find(Builders<Users>.Filter.Eq("Username", username)).FirstOrDefaultAsync(); 
            var userByEmail = await _collection.Find(Builders<Users>.Filter.Eq("Email", email)).FirstOrDefaultAsync();

            return userByUsername != null || userByEmail != null;
        }
        public async Task<Users> GetByUsernameAsync(string username)
        {
            var filter = Builders<Users>.Filter.Eq(u => u.Username, username);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}