using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace laget.Auditing.Resubmitter
{
    public class Service : IHostedService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        private readonly string _subscriberName;
        private readonly string _topicName;

        public Service(IConfiguration configuration)
        {
            _subscriberName = configuration.GetValue<string>("ServiceBus:SubscriberName");
            _topicName = configuration.GetValue<string>("ServiceBus:TopicName");
            _client = new ServiceBusClient(configuration.GetValue<string>("ServiceBus:ConnectionString"));
            _sender = _client.CreateSender(_topicName);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var dlqReceiver = _client.CreateReceiver(_topicName, _subscriberName, new ServiceBusReceiverOptions
                {
                    SubQueue = SubQueue.DeadLetter,
                    ReceiveMode = ServiceBusReceiveMode.PeekLock
                });

                await ProcessDeadLetterMessagesAsync($"topic: {_topicName} -> subscriber: {_subscriberName}", 1000, dlqReceiver);
            }
            catch (Azure.Messaging.ServiceBus.ServiceBusException ex)
            {
                if (ex.Reason == Azure.Messaging.ServiceBus.ServiceBusFailureReason.MessagingEntityNotFound)
                {
                    Log.Error(ex, $"Topic:Subscriber '{_topicName}:{_subscriberName}' not found. Check that the name provided is correct.");
                }
                else
                {
                    throw;
                }
            }

            Log.Information("Done!!!");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _sender.CloseAsync(cancellationToken);
            await _sender.DisposeAsync();
            await _client.DisposeAsync();

            Environment.Exit(0);
        }

        private async Task ProcessDeadLetterMessagesAsync(string source, int fetchCount, ServiceBusReceiver dlqReceiver)
        {
            var wait = new TimeSpan(0, 0, 10);

            Log.Information($"fetching messages ({wait.TotalSeconds} seconds retrieval timeout)");
            Log.Information(source);

            var dlqMessages = await dlqReceiver.ReceiveMessagesAsync(fetchCount, wait);

            Log.Information($"dl-count: {dlqMessages.Count}");

            var i = 1;

            foreach (var dlqMessage in dlqMessages)
            {
                Log.Information($"start processing message {i}");
                Log.Information($"dl-message-dead-letter-message-id: {dlqMessage.MessageId}");
                Log.Information($"dl-message-dead-letter-reason: {dlqMessage.DeadLetterReason}");
                Log.Information($"dl-message-dead-letter-error-description: {dlqMessage.DeadLetterErrorDescription}");

                var resubmittableMessage = new ServiceBusMessage(dlqMessage);

                await _sender.SendMessageAsync(resubmittableMessage);

                await dlqReceiver.CompleteMessageAsync(dlqMessage);

                Log.Information($"finished processing message {i}");
                Log.Information("--------------------------------------------------------------------------------------");

                i++;
            }

            await dlqReceiver.CloseAsync();

            Log.Information($"finished");
        }
    }
}
