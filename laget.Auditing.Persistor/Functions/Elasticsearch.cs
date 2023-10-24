﻿using laget.Auditing.Sinks;
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
            _persistor = new Sinks.Elasticsearch.Persistor(Environment.GetEnvironmentVariable("ElasticsearchApiKey"), Environment.GetEnvironmentVariable("ElasticsearchApiUrl"));
        }

        [Function(nameof(Elasticsearch))]
        public void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBus")] Message message)
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

                _logger.LogInformation($"elasticsearch persisted {message.Name} {message}");
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
