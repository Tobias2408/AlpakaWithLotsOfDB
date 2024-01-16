using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestAlpakaMongo.Models;

namespace RestAlpakaMongo.Context;

public class AlpakaContext
{
    private readonly IMongoDatabase _database = null;

    public AlpakaContext(IOptions<Settings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
            _database = client.GetDatabase(settings.Value.Database);
    }

    public IMongoCollection<Alpaka> Alpakas
    {
        get
        {
            return _database.GetCollection<Alpaka>("Alpakas");
        }
    }
    
    public class Settings
    {
        public string ConnectionString;
        public string Database;
    }
}