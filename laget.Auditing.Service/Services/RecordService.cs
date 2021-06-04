using System;
using System.Threading;
using System.Threading.Tasks;
using laget.Auditing.Service.Handlers;
using laget.Azure.ServiceBus.Extensions;
using laget.Azure.ServiceBus.Topic;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace laget.Auditing.Service.Services
{
    public class RecordService : IHostedService
    {
        private readonly IRecordHandler _handler;
        private readonly ITopicReceiver _receiver;

        public RecordService(IConfiguration configuration, IRecordHandler handler)
        {
            _receiver = new TopicReceiver(configuration.GetValue<string>("ServiceBus:Url")
                    .Replace("{name}", configuration.GetValue<string>("ServiceBus:Auditing:Name"))
                    .Replace("{key}", configuration.GetValue<string>("ServiceBus:Auditing:Key")),
                new TopicOptions
                {
                    TopicName = "auditing",
                    SubscriptionName = "auditing-service",
                    ReceiveMode = ReceiveMode.PeekLock
                });
            _handler = handler;
        }

        public async Task StartAsync(CancellationToken ct)
        {
            Log.Information($"Starting '{nameof(RecordService)}'");

            await Task.Run(() =>
            {
                _receiver.Register(async (message, _) =>
                {
                    Log.Information($"Executing '{nameof(RecordService)}' (Reason='Trigger fired at {DateTime.Now}', Id='{message.MessageId}')");

                    var model = message.Deserialize<Models.Message>();

                    await _handler.Handle(model.Category.ToLower(), model.ToRecord);
                }, ExceptionHandler);
            }, ct);
        }

        public Task StopAsync(CancellationToken ct)
        {
            return Task.CompletedTask;
        }

        private static Task ExceptionHandler(ExceptionReceivedEventArgs ex)
        {
            Log.Error(ex.Exception, $"{ex.Exception.Message} (Type='{ex.Exception.GetType()}' EntityPath='{ex.ExceptionReceivedContext.EntityPath}')");

            return Task.CompletedTask;
        }
    }
}
