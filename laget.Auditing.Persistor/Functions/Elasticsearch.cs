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
            _persistor = new Sinks.Elasticsearch.Persistor(Environment.GetEnvironmentVariable("ElasticsearchUrl"));
        }

        [FunctionName("Elasticsearch")]
        public void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBus")] Message message, ILogger log)
        {
            try
            {
                DogStatsd.Counter("sink.elasticsearch.message.received", 1);

                using (DogStatsd.StartTimer("sink.elasticsearch.persistence"))
                {
                    _persistor.Persist(message.Name, message);
                }

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
