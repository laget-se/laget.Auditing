using System;
using System.Threading.Tasks;
using laget.Auditing.Models.Converters;
using laget.Azure.ServiceBus.Topic;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace laget.Auditing
{
    public interface IAuditor
    {
        void Send(Models.Message msg);
        Task SendAsync(Models.Message msg);
    }

    public class Auditor : IAuditor
    {
        private readonly ITopicSender _topicSender;

        public Auditor(ITopicSender topicSender)
        {
            _topicSender = topicSender;
        }

        public Auditor(string connectionString, string topicName = "auditing")
        {
            _topicSender = new TopicSender(connectionString,
                new TopicOptions
                {
                    TopicName = topicName,
                    RetryPolicy = new RetryExponential(minimumBackoff: TimeSpan.FromSeconds(5), maximumBackoff: TimeSpan.FromMinutes(5), maximumRetryCount: 100)
                });
        }

        public Auditor(string connectionString, TopicOptions options)
        {
            _topicSender = new TopicSender(connectionString, options);
        }

        public Auditor(TopicSender topicSender)
        {
            _topicSender = topicSender;
        }

        public void Send(Models.Message message)
        {
            Task.Run(() => SendAsync(message)).Wait();
        }

        public async Task SendAsync(Models.Message message)
        {
            var json = JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                Converters = new JsonConverter[] { new AuditingConverter() },
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessDictionaryKeys = true,
                        ProcessExtensionDataNames = true,
                        OverrideSpecifiedNames = true
                    }
                },
                Formatting = Formatting.Indented
            });

            await _topicSender.SendAsync(json);
        }
    }
}
