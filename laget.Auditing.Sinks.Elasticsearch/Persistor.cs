﻿using Elasticsearch.Net;
using laget.Auditing.Sinks.Elasticsearch.Attributes;
using laget.Auditing.Sinks.Elasticsearch.Models;
using Microsoft.Extensions.Logging;
using Nest;
using System;

namespace laget.Auditing.Sinks.Elasticsearch
{
    public class Persistor : IPersistor<Message>
    {
        private readonly IElasticClient _client;
        private readonly ILogger _logger;

        public Persistor(ILogger logger, string apiKey, string apiUrl)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl))
            {
                Configured = false;
                return;
            }

            var uri = new Uri(apiUrl);
            var nodes = new[] { uri };
            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool)
                .ApiKeyAuthentication(new ApiKeyAuthenticationCredentials(apiKey));

            _client = new ElasticClient(settings);
            _logger = logger;
        }

        public bool Configured { get; } = true;

        public void Persist(string indexName, Message message)
        {
            try
            {
                EnsureIndex(message);

                var result = _client.Index(message, x => x.Index(GetIndexName(message)));

                if (!result.IsValid)
                {
                    _logger.LogError($"{result.DebugInformation}", result.OriginalException);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private void EnsureIndex(Message message)
        {
            var name = GetIndexName(message);
            var index = _client.Indices.Exists(name);

            if (!index.Exists)
            {
                _client.Indices.Create(name, f => f.Settings(x => x.NumberOfShards(1).NumberOfReplicas(0)).Map<Message>(m => m.AutoMap()));
            }
        }

        private static string GetIndexName(Message message)
        {
            var attribute = (IndexAttribute)Attribute.GetCustomAttribute(message.GetType(), typeof(IndexAttribute));
            var format = attribute.IndexFormat;
            var name = "auditing";

            if (format != null)
                name += $"-{DateTime.Now.ToString(format)}-0";

            return name;
        }
    }
}
