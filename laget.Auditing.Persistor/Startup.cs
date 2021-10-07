using laget.Auditing.Persistor;
using laget.Auditing.Persistor.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
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
        }
    }   
}