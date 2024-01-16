using RestAlpakaMongo.Models;

using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;

namespace RestAlpakaMongo.Services
{
    public class TicketsService : MongoDbService<Tickets>
    {
        public TicketsService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Tickets")
        {

        }
    
    }
}
