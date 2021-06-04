using System;
using System.Threading;
using System.Threading.Tasks;
using laget.Auditing.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace laget.Auditing.Service.Handlers
{
    public interface IRecordHandler
    {
        Task Handle(string collectionName, Record record);
    }

    public class RecordHandler : IRecordHandler
    {
        private readonly IMongoDatabase _database;

        public RecordHandler(IConfiguration configuration)
        {
            var url = new MongoUrl(configuration.GetConnectionString("MongoConnectionString"));
            var client = new MongoClient(url);

            _database = client.GetDatabase(url.DatabaseName, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.Acknowledged
            });
        }

        public async Task Handle(string collectionName, Record record)
        {
            Log.Information($"Executing '{nameof(RecordHandler)}' (Reason='Trigger fired at {DateTime.Now}', Id='{Thread.CurrentThread.ManagedThreadId}')");

            try
            {
                var collection = _database.GetCollection<Record>(collectionName);
                await collection.InsertOneAsync(record);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }
    }
}
