using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using RestAlpaka.Model;

namespace Neoflix.Services
{
    public class LocationService
    {
        private readonly IDriver _driver;

        public LocationService(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<Location> CreateAsync(Location location)
        {
            var checkQuery = "MATCH (l:Location {LocationId: $locationId}) RETURN l";
            using (var session = _driver.AsyncSession())
            {
                var checkResult = await session.RunAsync(checkQuery, new { locationId = location.LocationId });
                if (await checkResult.FetchAsync()) // Check if there's a record
                {
                    throw new InvalidOperationException("Location with the given LocationId already exists.");
                }
            }
            

                    var createQuery = @"
            CREATE (l:Location {
                LocationId: $locationId, 
                City: $city, 
                Address: $address, 
                Postalcode: $postalcode
            }) 
            RETURN l";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(createQuery, new { locationId = location.LocationId, city = location.City, address = location.Address, postalcode = location.Postalcode });
                if (await result.FetchAsync()) // FetchAsync returns true if there's a record
                {
                    var record = result.Current;
                    var node = record[0].As<INode>(); // Access the node

                    return new Location
                    {
                        LocationId = node.Properties["LocationId"].As<int>(),
                        City = node.Properties["City"].As<string>(),
                        Address = node.Properties["Address"].As<string>(),
                        Postalcode = node.Properties["Postalcode"].As<string>()
                    };
                }
                else
                {
                    throw new InvalidOperationException("Location creation failed, no record returned.");
                }
            
            }
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var query = "MATCH (l:Location) RETURN l";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query);
                var records = await result.ToListAsync();
                var locations = new List<Location>();

                foreach (var record in records)
                {
                    var node = record[0].As<INode>();
                    locations.Add(new Location
                    {
                        LocationId = node.Properties["LocationId"].As<int>(),
                        City = node.Properties["City"].As<string>(),
                        Address = node.Properties["Address"].As<string>(),
                        Postalcode = node.Properties["Postalcode"].As<string>()
                    });
                }

                return locations;
            }
        }

        public async Task<Location> GetByIdAsync(int locationId)
        {
            var query = "MATCH (l:Location {LocationId: $locationId}) RETURN l";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, new { locationId });
                if (await result.FetchAsync())
                {
                    var record = result.Current;
                    var node = record["l"].As<INode>(); // Assuming 'l' is the alias for Location node

                    return new Location
                    {
                        LocationId = node.Properties["LocationId"].As<int>(),
                        City = node.Properties["City"].As<string>(),
                        Address = node.Properties["Address"].As<string>(),
                        Postalcode = node.Properties["Postalcode"].As<string>()
                    };
                }
                return null; // Return null if no location is found
            }
        }


        public async Task<Location> UpdateAsync(Location location)
        {
            var query = @"
        MATCH (l:Location {LocationId: $locationId})
        OPTIONAL MATCH (l)-[r:RELATED_TO]->() // Define the relationship type(s) you want to delete
        DELETE r
        SET l.City = $city, 
            l.Address = $address, 
            l.Postalcode = $postalcode
        RETURN l.LocationId, l.City, l.Address, l.Postalcode";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, new
                {
                    locationId = location.LocationId, // Make sure LocationId is provided in the parameters
                    city = location.City,
                    address = location.Address,
                    postalcode = location.Postalcode
                });

                if (await result.FetchAsync())
                {
                    var record = result.Current;
                    return new Location
                    {
                        LocationId = record["l.LocationId"].As<int>(), // Corrected parameter name
                        City = record["l.City"].As<string>(), // Corrected parameter name
                        Address = record["l.Address"].As<string>(), // Corrected parameter name
                        Postalcode = record["l.Postalcode"].As<string>() // Corrected parameter name
                    };
                }
                else
                {
                    throw new InvalidOperationException("Location update failed, no record returned.");
                }
            }
        }




        public async Task<bool> DeleteAsync(int locationId)
        {
            var query = "MATCH (l:Location {LocationId: $locationId}) DELETE l";

            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(query, new { locationId });
                var summary = await result.ConsumeAsync();
                return summary.Counters.NodesDeleted > 0;
            }
        }
    }
}
