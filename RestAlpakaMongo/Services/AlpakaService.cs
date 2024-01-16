using MongoDB.Driver;
using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Models;
using System.Threading.Tasks;
using RestAlpakaMongo.DTOs;

namespace RestAlpakaMongo.Services
{
    public class AlpakaService : MongoDbService<Alpaka>
    {
        public AlpakaService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Alpakas")
        {
        }

        public async Task<Alpaka> GetByAlpakaNameAsync(string alpakaName)
        {
            var filter = Builders<Alpaka>.Filter.Eq("AlpakaName", alpakaName);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Alpaka> CreateOrUpdateAlpakaAsync(AlpakaDto alpakaDto)
        {
            var existingAlpaka = await GetByAlpakaNameAsync(alpakaDto.AlpakaName);
            if (existingAlpaka != null)
            {
                // Update the existing alpaka
                existingAlpaka.Color = alpakaDto.Color;
                existingAlpaka.Age = alpakaDto.Age;
                existingAlpaka.Description = alpakaDto.Description;
                var filter = Builders<Alpaka>.Filter.Eq("AlpakaName", alpakaDto.AlpakaName);
                await _collection.ReplaceOneAsync(filter, existingAlpaka);
                return existingAlpaka;
            }
            else
            {
                // Create a new alpaka
                var newAlpaka = new Alpaka
                {
                    AlpakaName = alpakaDto.AlpakaName,
                    Color = alpakaDto.Color,
                    Age = alpakaDto.Age,
                    Description = alpakaDto.Description
                };
                await _collection.InsertOneAsync(newAlpaka);
                return newAlpaka;
            }
        }
    }
}