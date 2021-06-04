using System.Collections.Generic;
using System.Threading.Tasks;
using laget.Auditing.Models;
using MongoDB.Driver;

namespace laget.Auditing.Auditor
{
    public interface IReporter<TEntity>
    {
        IEnumerable<Record> Find(FilterDefinition<Record> filter);
        Task<IEnumerable<Record>> FindAsync(FilterDefinition<Record> filter);
    }

    public class Reporter<TEntity> : IReporter<TEntity>
    {
        protected readonly IMongoCollection<Record> Collection;
        protected readonly string CollectionName = typeof(TEntity).Name.ToLower();

        public Reporter(MongoUrl url)
        {
            var client = new MongoClient(url);
            var database = client.GetDatabase(url.DatabaseName, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred
            });

            Collection = database.GetCollection<Record>(CollectionName);
        }

        public Reporter(MongoUrl url, string collectionName)
            : this(url)
        {
            CollectionName = collectionName;
        }

        public Reporter(IMongoCollection<Record> collection)
        {
            Collection = collection;
        }

        public IEnumerable<Record> Find(FilterDefinition<Record> filter)
        {
            return Collection.Find(filter).ToList();
        }

        public async Task<IEnumerable<Record>> FindAsync(FilterDefinition<Record> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }
    }
}
