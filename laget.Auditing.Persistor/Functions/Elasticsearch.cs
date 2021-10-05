//using laget.Auditing.Persistor.Models;
//using laget.Auditing.Sinks;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Extensions.Logging;

//namespace laget.Auditing.Persistor.Functions
//{
//    public class Elasticsearch
//    {
//        private readonly IPersistor _persistor;

//        public Elasticsearch()
//        {
//            _persistor = new Sinks.Elasticsearch.Persistor();
//        }

//        [FunctionName("Elasticsearch")]
//        public void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBusConnectionString")]Message message, ILogger log)
//        {
//            log.LogInformation($"C# ServiceBus topic trigger function processed message: {message}");
//        }
//    }
//}
