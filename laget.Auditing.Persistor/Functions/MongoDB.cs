using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace laget.Auditing.Persistor.Functions
{
    public static class MongoDB
    {
        [FunctionName("MongoDB")]
        public static void Run([ServiceBusTrigger("auditing", "sink-mongodb", Connection = "AzureServiceBusConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
