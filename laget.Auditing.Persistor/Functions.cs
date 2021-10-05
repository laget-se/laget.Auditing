using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace laget.Auditing.Persistor
{
    public static class Functions
    {
        [FunctionName("Elasticsearch")]
        public static void RunForElasticsearch([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBusConnectionString")] string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }

        [FunctionName("MongoDB")]
        public static void RunForMongoDB([ServiceBusTrigger("auditing", "sink-mongodb", Connection = "AzureServiceBusConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
