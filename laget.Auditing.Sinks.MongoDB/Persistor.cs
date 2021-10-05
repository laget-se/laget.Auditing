using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace laget.Auditing.Sinks.MongoDB
{
    public class Persistor : IPersistor
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

        public void Persist(string collectionName, object @object)
        {
            try
            {
                var collection = _database.GetCollection<object>(collectionName.ToLower());

                collection.InsertOne(@object);
            }
            catch (Exception ex)
            {
                //TODO: Log error?
                //Log.Error(ex, ex.Message);
            }
        }
    }
}
