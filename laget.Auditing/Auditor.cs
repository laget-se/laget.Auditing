using System.Threading.Tasks;
using laget.Auditing.Auditor.Converters;
using laget.Auditing.Models;
using laget.Azure.ServiceBus.Topic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace laget.Auditing.Auditor
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
            _topicSender = new TopicSender(connectionString, new TopicOptions { TopicName = topicName });
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
