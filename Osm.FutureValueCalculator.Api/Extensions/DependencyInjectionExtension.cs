using Microsoft.Extensions.DependencyInjection;
using Osm.FutureValueCalculator.App.Apps;
using Osm.FutureValueCalculator.App.Interfaces;
using Osm.FutureValueCalculator.Domain.Interfaces;
using Osm.FutureValueCalculator.Domain.Services;
using System.Net.Http;

namespace Osm.FutureValueCalculator.Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IFutureValueService, FutureValueService>();
            services.AddScoped<IFutureValueApp, FutureValueApp>();
            services.AddScoped<IInterestRateApp, InterestRateApp>();            

            return services;
        }
    }
}
