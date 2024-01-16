using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RestAlpakaMongo.GenericBase
{
    public abstract class MongoDbService<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        protected MongoDbService(IMongoClient client, IConfiguration config, string collectionName)
        {
            var database = client.GetDatabase(config["MongoDBConnection:Database"]);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity, IClientSessionHandle session = null)
        {
            if (session != null)
            {
                await _collection.InsertOneAsync(session, entity);
            }
            else
            {
                await _collection.InsertOneAsync(entity);
            }
        }

        public async Task UpdateAsync(string id, T entity, IClientSessionHandle session = null)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            if (session != null)
            {
                await _collection.ReplaceOneAsync(session, filter, entity);
            }
            else
            {
                await _collection.ReplaceOneAsync(filter, entity);
            }
        }

        public async Task DeleteAsync(string id, IClientSessionHandle session = null)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            if (session != null)
            {
                await _collection.DeleteOneAsync(session, filter);
            }
            else
            {
                await _collection.DeleteOneAsync(filter);
            }
        }
    }
}
