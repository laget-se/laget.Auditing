using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using StatsdClient;

namespace laget.Auditing.Persistor.Extensions
{
    //TODO: Move this to a NuGet?
    public static class FunctionsHostBuilderExtensions
    {
        public static IFunctionsHostBuilder UseDogStatsd(this IFunctionsHostBuilder builder, StatsdConfig config)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            ConfigureDogStatsd(config);

            return builder;
        }

        private static void ConfigureDogStatsd(StatsdConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            DogStatsd.Configure(config);
        }
    }
}
