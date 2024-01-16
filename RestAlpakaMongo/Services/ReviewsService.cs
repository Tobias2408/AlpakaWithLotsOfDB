using RestAlpakaMongo.Models;


using RestAlpakaMongo.GenericBase;
using RestAlpakaMongo.Interfaces;
using MongoDB.Driver;

namespace RestAlpakaMongo.Services
{
    public class ReviewsService : MongoDbService<Reviews>
    {
        public ReviewsService(IConfiguration config)
            : base(new MongoClient(config.GetConnectionString("DefaultConnection")), config, "Reviews")
        {

        }
    
    }
}
