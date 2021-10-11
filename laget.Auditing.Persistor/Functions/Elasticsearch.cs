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
                DogStatsd.Counter("sink.elasticsearch.message", 1);

                using (DogStatsd.StartTimer("sink.elasticsearch.message"))
                {
                    _persistor.Persist(message.Name, message);
                }

                log.LogInformation($@"Elasticsearch persisted { message.Name } { message }");
            }
            catch (Exception ex)
            {
                log.LogError($@"DogStatsd persist failed, { ex } { ex.Message } ");
                throw;
            }

        }
    }
}
