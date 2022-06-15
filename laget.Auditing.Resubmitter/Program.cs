using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace laget.Auditing.Resubmitter
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Service>();
                })
                .UseConsoleLifetime()
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
                .UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .Build()
                .RunAsync();
        }
    }
}
