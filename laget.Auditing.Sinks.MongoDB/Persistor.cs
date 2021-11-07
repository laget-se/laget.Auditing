using System.Collections.Generic;
using laget.Auditing.Sinks.MongoDB.Models;
using MongoDB.Driver;

namespace laget.Auditing.Sinks.MongoDB
{
    public class Persistor : IPersistor<Message>
    {
        private readonly IMongoDatabase _database;

        public Persistor(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            _database = client.GetDatabase(url.DatabaseName, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.Acknowledged
            });
        }

        public void Persist(string collectionName, Message message)
        {
            var collection = _database.GetCollection<Message>(collectionName.ToLower());

            EnsureIndexes(collection);

            collection.InsertOne(message);
        }

        private static void EnsureIndexes(IMongoCollection<Message> collection)
        {
            var builder = Builders<Message>.IndexKeys;
            var indexes = new List<CreateIndexModel<Message>>
            {
                new CreateIndexModel<Message>(builder.Ascending(_ => _.Action), new CreateIndexOptions { Background = true }),
                new CreateIndexModel<Message>(builder.Ascending(_ => _.ClubId), new CreateIndexOptions { Background = true }),
                new CreateIndexModel<Message>(builder.Ascending(_ => _.CreatedAt), new CreateIndexOptions { Background = true }),
                new CreateIndexModel<Message>(builder.Ascending(_ => _.SiteId), new CreateIndexOptions { Background = true })
            };

            collection.Indexes.CreateMany(indexes);
        }
    }
}
