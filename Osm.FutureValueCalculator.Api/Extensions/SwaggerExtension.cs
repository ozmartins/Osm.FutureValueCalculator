using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Osm.FutureValueCalculator.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Osm.InterestRate.Api", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Osm.InterestRate.Api v1"));

            var option = new RewriteOptions();

            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            return app;
        }
    }
}
