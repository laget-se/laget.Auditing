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
            var collection = _database.GetCollection<object>(collectionName.ToLower());

            collection.InsertOne(message);
        }
    }
}
