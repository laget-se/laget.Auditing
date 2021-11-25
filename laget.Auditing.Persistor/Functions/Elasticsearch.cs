using System;
using laget.Auditing.Sinks;
using laget.Auditing.Sinks.Elasticsearch.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StatsdClient;

namespace laget.Auditing.Persistor.Functions
{
    public class Elasticsearch
    {
        private readonly IPersistor<Message> _persistor;

        public Elasticsearch()
        {
            var apiKey = Environment.GetEnvironmentVariable("ElasticsearchApiKey");
            var apiUrl = Environment.GetEnvironmentVariable("ElasticsearchApiUrl");

            _persistor = new Sinks.Elasticsearch.Persistor(apiKey, apiUrl);
        }

        [FunctionName("Elasticsearch")]
        public void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBus")] Message message, ILogger log)
        {
            try
            {
                DogStatsd.Counter("sink.elasticsearch.message.received", 1);
                DogStatsd.Counter($"sink.elasticsearch.action.{message.Action}", 1);
                DogStatsd.Counter($"sink.elasticsearch.system.{message.System}", 1);
                DogStatsd.Counter($"sink.elasticsearch.system.{message.Name}.{message.Action}", 1);

                using (DogStatsd.StartTimer("sink.elasticsearch.persistence"))
                {
                    _persistor.Persist(message.Name, message);
                }

                DogStatsd.Counter("sink.elasticsearch.message.succeeded", 1);

                log.LogInformation($@"elasticsearch persisted { message.Name } { message }");
            }
            catch (Exception ex)
            {
                DogStatsd.Counter("sink.elasticsearch.message.failed", 1);
                log.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
