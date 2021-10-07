//using laget.Auditing.Sinks;
//using laget.Auditing.Sinks.Elasticsearch.Models;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Extensions.Logging;
//using StatsdClient;

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
//        public void Run([ServiceBusTrigger("auditing", "sink-elasticsearch", Connection = "AzureServiceBus")] Message message, ILogger log)
//        {
//            log.LogInformation($"C# ServiceBus topic trigger function processed message: {message}");
//            DogStatsd.Counter("sink.mongodb.message", 1);

//            _persistor.Persist(message.Name, message);
//        }
//    }
//}
