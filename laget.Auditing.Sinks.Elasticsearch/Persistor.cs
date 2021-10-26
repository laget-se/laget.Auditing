using System;
using laget.Auditing.Sinks.Elasticsearch.Attributes;
using laget.Auditing.Sinks.Elasticsearch.Models;
using Nest;

namespace laget.Auditing.Sinks.Elasticsearch
{
    public  class Persistor : IPersistor<Message>
    {
        private readonly IElasticClient _client;

        public Persistor(string connectionString)
        {
            var uri = new Uri(connectionString);
            var settings = new ConnectionSettings(uri);
            _client = new ElasticClient(settings);
        }

        public void Persist(string indexName, Message message)
        {
            EnsureIndex(message);

            _client.Index(message, x => x.Index(GetIndexName(message)));
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
            var name = $"auditing-{message.Name.ToLower()}";

            if (format != null)
                name += $"-{DateTime.Now.ToString(format)}";

            return name;
        }
    }
}
