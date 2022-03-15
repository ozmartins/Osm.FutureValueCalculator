using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Osm.FutureValueCalculator.App.Models;
using System.Diagnostics.CodeAnalysis;

namespace Osm.FutureValueCalculator.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AppSettingsExtensions
    {
        public static IServiceCollection AddCustomAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GitHubModel>(configuration.GetSection("GitHub"));

            services.Configure<HerokuModel>(configuration.GetSection("GitHub"));

            services.Configure<ShowMeTheCodeModel>(configuration.GetSection("ShowMeTheCode"));            

            services.AddOptions();

            return services;
        }        
    }
}
