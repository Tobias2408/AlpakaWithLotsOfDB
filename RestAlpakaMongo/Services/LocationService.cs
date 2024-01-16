using RestAlpakaMongo.Models;
using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace RestAlpakaMongo.Services
{
    public class LocationService : MongoDbService<Location>
    {
        public LocationService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Location")
        {

        }

        // Check if a location with the same details already exists
        public async Task<bool> LocationExists(Location location)
        {
            var filter = Builders<Location>.Filter.And(
                Builders<Location>.Filter.Eq("City", location.City),
                Builders<Location>.Filter.Eq("Address", location.Address),
                Builders<Location>.Filter.Eq("Postalcode", location.Postalcode)
            );

            var existingLocation = await _collection.Find(filter).FirstOrDefaultAsync();

            return existingLocation != null;
        }
    }
}