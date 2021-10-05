using System;
using laget.Auditing.Sinks;
using laget.Auditing.Sinks.MongoDB.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace laget.Auditing.Persistor.Functions
{
    public class MongoDB
    {
        private readonly IPersistor _persistor;

        public MongoDB()
        {
            _persistor = new Sinks.MongoDB.Persistor(Environment.GetEnvironmentVariable("MongoConnectionString"));
        }

        [FunctionName("MongoDB")]
        public void Run([ServiceBusTrigger("auditing", "sink-mongodb", Connection = "AzureServiceBus")]Message message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {message.ToString()}");

            _persistor.Persist(message.Name, message);
        }
    }
}
