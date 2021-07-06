using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using laget.Auditing.Service.Autofac;
using laget.Auditing.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StatsdClient;
using StatsdClient.Extensions.Hosting;

namespace laget.Auditing.Service
{
    internal class Program
    {
        private static async Task Main()
        {
            await Host.CreateDefaultBuilder()
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    builder.RegisterModule<HandlerModule>();
                    builder.RegisterModule<ServiceModule>();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<RecordService>();
                })
                .UseConsoleLifetime()
                .UseDogStatsd((context) => new StatsdConfig
                {
                    Prefix = "auditing",
                    StatsdServerName = context.HostingEnvironment.IsProduction() ? "stats.laget.se" : "stats.laget.dev"
                })
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
                .UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseWindowsService()
                .Build()
                .RunAsync();
        }
    }
}
