using Azure.Messaging.ServiceBus;
using laget.Auditing.Persistor.Extensions;
using laget.Auditing.Sinks;
using laget.Auditing.Sinks.Elasticsearch.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using StatsdClient;
using System;

namespace laget.Auditing.Persistor.Functions
{
    public class Elasticsearch
    {
        private readonly ILogger<Elasticsearch> _logger;
        private readonly IPersistor<Message> _persistor;

        public Elasticsearch(ILogger<Elasticsearch> logger)
        {
            _logger = logger;
            _persistor = new Sinks.Elasticsearch.Persistor(logger, Environment.GetEnvironmentVariable("ElasticsearchApiKey"), Environment.GetEnvironmentVariable("ElasticsearchApiUrl"));
        }

        [Function(nameof(Elasticsearch))]
        public void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBus")] ServiceBusReceivedMessage message, ServiceBusMessageActions actions)
        {
            try
            {
                var model = message.Deserialize<Message>();

                DogStatsd.Counter("sink.elasticsearch.message.received", 1);
                DogStatsd.Counter($"sink.elasticsearch.action.{model.Action}", 1);
                DogStatsd.Counter($"sink.elasticsearch.system.{model.System}", 1);
                DogStatsd.Counter($"sink.elasticsearch.system.{model.Name}.{model.Action}", 1);

                if (_persistor.Configured)
                {
                    using (DogStatsd.StartTimer("sink.elasticsearch.persistence"))
                    {
                        _persistor.Persist(model.Name, model);
                    }
                }
                else
                {
                    actions.DeadLetterMessageAsync(message);
                }

                DogStatsd.Counter("sink.elasticsearch.message.succeeded", 1);

                _logger.LogInformation($"elasticsearch persisted {model.Name} {model}");
            }
            catch (Exception ex)
            {
                DogStatsd.Counter("sink.elasticsearch.message.failed", 1);
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
