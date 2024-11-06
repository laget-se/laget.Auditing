using Microsoft.Extensions.Hosting;
using StatsdClient;
using StatsdClient.Extensions.Hosting;
using System;

namespace laget.Auditing.Persistor
{
    internal class Program
    {
        internal static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .Run();

        internal static IHostBuilder CreateHostBuilder(string[] args) => new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(s =>
            {
            })
            .UseDogStatsd(new StatsdConfig
            {
                Prefix = "auditing",
                StatsdServerName = "stats.laget.se",
                ClientSideAggregation = new ClientSideAggregationConfig
                {
                    FlushInterval = TimeSpan.FromMinutes(1)
                }
            });
    }
}
