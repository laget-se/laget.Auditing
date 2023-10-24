using Azure.Messaging.ServiceBus;
using laget.Auditing.Sinks;
using Newtonsoft.Json;

namespace laget.Auditing.Persistor.Extensions
{
    internal static class ServiceBusReceivedMessageExtensions
    {
        public static TEntity Deserialize<TEntity>(this ServiceBusReceivedMessage message) where TEntity : IMessage
        {
            var entity = JsonConvert.DeserializeObject<TEntity>(message.Body.ToString(),
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

            return entity;
        }
    }
}
