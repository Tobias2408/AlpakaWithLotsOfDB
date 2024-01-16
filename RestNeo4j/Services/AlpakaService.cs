using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using RestAlpaka.Model;

namespace Neoflix.Services
{
    public class AlpakaService
    {
        private readonly IDriver _driver;

        public AlpakaService(IDriver driver)
        {
            _driver = driver;
        }

        // Create a new Alpaka
        // Create a new Alpaka
        public async Task<Alpaka> CreateAsync(Alpaka alpaka)
        {
            var query = "CREATE (a:Alpaka {Id: $id, Name: $name, Color: $color, Age: $age, Description: $description}) RETURN a";
            var parameters = new { id = alpaka.Id, name = alpaka.Name, color = alpaka.Color, age = alpaka.Age, description = alpaka.Description };

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, parameters);
                var record = await result.SingleAsync();

                // Access the properties using uppercase keys
                var createdAlpaka = new Alpaka
                {
                    Id = record["Id"].As<int>(),
                    Name = record["Name"].As<string>(),
                    Color = record["Color"].As<string>(),
                    Age = record["Age"].As<int>(),
                    Description = record["Description"].As<string>()
                };
                Console.WriteLine(createdAlpaka.ToString());
                return createdAlpaka;
            }
        }




        

        // Get a single Alpaka by id
        public async Task<Alpaka> GetAsync(int id)
        {
            var query = "MATCH (a:Alpaka {Id: $id}) RETURN a.Id AS Id, a.Name AS Name, a.Color AS Color, a.Age AS Age, a.Description AS Description";
            var parameters = new { id };

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, parameters);
                var record = await result.SingleAsync();
        
                // Now access the properties using the correct keys
                var alpaka = new Alpaka
                {
                    Id = record["Id"].As<int>(),
                    Name = record["Name"].As<string>(),
                    Color = record["Color"].As<string>(),
                    Age = record["Age"].As<int>(),
                    Description = record["Description"].As<string>()
                };

                return alpaka;
            }
        }


        // Update an Alpaka
        // Update an Alpaka
        public async Task UpdateAsync(int id, Alpaka updatedAlpaka)
        {
            var query = "MATCH (a:Alpaka {Id: $id}) SET a.Name = $name, a.Color = $color, a.Age = $age, a.Description = $description";
    
            var parameters = new
            {
                id,
                name = updatedAlpaka.Name,
                color = updatedAlpaka.Color,
                age = updatedAlpaka.Age,
                description = updatedAlpaka.Description
            };

            using (var session = _driver.AsyncSession())
            {
                await session.RunAsync(query, parameters);
            }
        }






        // Delete an Alpaka
        public async Task<bool> DeleteAsync(int id)
        {
            var query = "MATCH (a:Alpaka {alpaka: $id}) DETACH DELETE a";
            var parameters = new { id };

            using (var session = _driver.AsyncSession())
            {
                await session.RunAsync(query, parameters);
                return true; // Assuming the delete operation was successful
            }
        }

        // Get all Alpakas
        public async Task<IEnumerable<Alpaka>> GetAllAsync()
        {
            var query = "MATCH (a:Alpaka) RETURN a";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query);
                var alpakas = await result.ToListAsync(record => record["a"].As<Alpaka>());
                return alpakas;
            }
        }
    }
}
