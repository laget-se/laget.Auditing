﻿using System;
using laget.Auditing.Sinks;
using laget.Auditing.Sinks.MongoDB.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StatsdClient;

namespace laget.Auditing.Persistor.Functions
{
    public class MongoDB
    {
        private readonly IPersistor<Message> _persistor;

        public MongoDB()
        {
            _persistor = new Sinks.MongoDB.Persistor(Environment.GetEnvironmentVariable("MongoConnectionString"));
        }

        [FunctionName("MongoDB")]
        public void Run([ServiceBusTrigger("auditing", "sink-mongodb", Connection = "AzureServiceBus")] Message message, ILogger log)
        {
            try
            {
                DogStatsd.Counter("sink.mongodb.message", 1);

                using (DogStatsd.StartTimer("sink.mongodb.message"))
                {
                    _persistor.Persist(message.Name, message);
                }

                log.LogInformation($@"Persisted { message.Name } { message }");
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                throw;
            }         
        }
    }
}       