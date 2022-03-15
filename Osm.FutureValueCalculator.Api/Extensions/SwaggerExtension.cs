using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
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
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Future value calculator API",
                    Description = "This simple API has two endpoints. "+
                                  "The first receives a present value and a number of months. "+
                                  "Using these two pieces of information (and the interest rate returned by InterestRateApi) this first endpoint calculates the future value of an amount of money. "+
                                  "The second endpoint just returns the URL for the project source code and the URL for the production running app.",
                    Contact = new OpenApiContact
                    {
                        Name = "Oséias da Silva Martins",
                        Url = new Uri("https://github.com/ozmartins/Osm.FutureValueCalculator"),
                        Email = "oseias.silva.martins@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
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
