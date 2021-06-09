using System;
using System.Threading.Tasks;
using laget.Auditing.Converters;
using laget.Azure.ServiceBus.Topic;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Message = laget.Auditing.Models.Message;

namespace laget.Auditing
{
    public interface IAuditor
    {
        void Send(Message msg);
        Task SendAsync(Message msg);
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
                    RetryPolicy = new RetryExponential(minimumBackoff: TimeSpan.Zero, maximumBackoff: TimeSpan.FromMinutes(5), maximumRetryCount: 100)
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

        public void Send(Message message)
        {
            Task.Run(() => SendAsync(message)).Wait();
        }

        public async Task SendAsync(Message message)
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
