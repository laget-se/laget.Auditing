using laget.Auditing.Sinks.MongoDB.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace laget.Auditing.Sinks.MongoDB
{
    public class Persistor : IPersistor<Message>
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger _logger;
        private readonly string _prefix;

        public Persistor(ILogger logger, string connectionString, string prefix = "")
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Configured = false;
                return;
            }

            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            _database = client.GetDatabase(url.DatabaseName, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.Acknowledged
            });
            _logger = logger;
            _prefix = prefix;
        }

        public bool Configured { get; } = true;

        public void Persist(string collectionName, Message message)
        {
            try
            {
                if (!string.IsNullOrEmpty(_prefix))
                    collectionName = $"{_prefix}.{collectionName}".ToLower();

                var collection = _database.GetCollection<Message>(collectionName.ToLower());

                EnsureIndexes(collection);

                collection.InsertOne(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private static void EnsureIndexes(IMongoCollection<Message> collection)
        {
            var builder = Builders<Message>.IndexKeys;
            var indexes = new List<CreateIndexModel<Message>>
            {
                new CreateIndexModel<Message>(builder.Ascending(_ => _.Action), new CreateIndexOptions { Background = true }),
                new CreateIndexModel<Message>(builder.Ascending(_ => _.ClubId), new CreateIndexOptions { Background = true }),
                new CreateIndexModel<Message>(builder.Ascending(_ => _.SiteId), new CreateIndexOptions { Background = true }),
                new CreateIndexModel<Message>(builder.Ascending(_ => _.System), new CreateIndexOptions { Background = true })
            };

            collection.Indexes.CreateMany(indexes);
        }
    }
}
