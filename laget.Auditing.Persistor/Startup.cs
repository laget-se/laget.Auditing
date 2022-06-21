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
            builder.UseDogStatsd(new StatsdConfig
            {
                Prefix = "auditing",
                StatsdServerName = "stats.laget.se"
            });

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger();

            builder.Services.AddLogging(lb => lb.AddSerilog(logger));
        }
    }
}