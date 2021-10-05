using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace laget.Auditing.Persistor.Functions
{
    public static class Elasticsearch
    {
        [FunctionName("Elasticsearch")]
        public static void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBusConnectionString")] string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
