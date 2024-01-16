using RestAlpakaMongo.Models;

using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;

namespace RestAlpakaMongo.Services
{
    public class PaymentsService : MongoDbService<Payments>
    {
        public PaymentsService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Payments")
        {

        }
    
    }
}
