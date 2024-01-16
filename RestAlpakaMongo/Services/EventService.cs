using RestAlpakaMongo.Models;

using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;


namespace RestAlpakaMongo.Services
{
    public class EventService : MongoDbService<Event>
    {
        public EventService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Event")
        {

        }
    }
}
