using laget.Auditing.Persistor;
using laget.Auditing.Persistor.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StatsdClient;

[assembly: FunctionsStartup(typeof(Startup))]

namespace laget.Auditing.Persistor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();

            builder.UseDogStatsd(new StatsdConfig
            {
                Prefix = "auditing",
                StatsdServerName = context.EnvironmentName == "Production" ? "stats.laget.se" : "stats.laget.dev"
            });

            var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
            .CreateLogger();

            builder.Services.AddLogging(lb => lb.AddSerilog(logger));
        }
    }   
}