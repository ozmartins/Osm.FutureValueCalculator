using Microsoft.Extensions.DependencyInjection;

namespace Osm.FutureValueCalculator.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {           
            return services;
        }
    }
}
