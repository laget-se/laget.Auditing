using Azure.Messaging.ServiceBus;
using laget.Auditing.Persistor.Extensions;
using laget.Auditing.Sinks;
using laget.Auditing.Sinks.MongoDB.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using StatsdClient;
using System;

namespace laget.Auditing.Persistor.Functions
{
    public class MongoDB
    {
        private readonly ILogger<MongoDB> _logger;
        private readonly IPersistor<Message> _persistor;

        public MongoDB(ILogger<MongoDB> logger)
        {
            _logger = logger;
            _persistor = new Sinks.MongoDB.Persistor(logger, Environment.GetEnvironmentVariable("MongoConnectionString"), Environment.GetEnvironmentVariable("MongoPrefix"));
        }

        [Function(nameof(MongoDB))]
        public void Run([ServiceBusTrigger("auditing", "sink-mongodb", Connection = "AzureServiceBus")] ServiceBusReceivedMessage message, ServiceBusMessageActions actions)
        {
            try
            {
                var model = message.Deserialize<Message>();

                DogStatsd.Counter("sink.mongodb.message.received", 1);
                DogStatsd.Counter($"sink.mongodb.action.{model.Action}", 1);
                DogStatsd.Counter($"sink.mongodb.system.{model.System}", 1);
                DogStatsd.Counter($"sink.mongodb.system.{model.Name}.{model.Action}", 1);

                if (_persistor.Configured)
                {
                    using (DogStatsd.StartTimer("sink.mongodb.persistence"))
                    {
                        _persistor.Persist(model.Name, model);
                    }
                }
                else
                {
                    actions.DeadLetterMessageAsync(message);
                }

                DogStatsd.Counter("sink.mongodb.message.succeeded", 1);

                _logger.LogInformation($@"mongodb persisted {model.Name} {model}");
            }
            catch (Exception ex)
            {
                actions.DeadLetterMessageAsync(message);

                DogStatsd.Counter("sink.mongodb.message.failed", 1);
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}