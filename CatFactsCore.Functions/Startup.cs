using System;
using CatFactsCore.Data;
using CatFactsCore.Domain;
using CatFactsCore.Domain.Integrations.CatFactNinja;
using CatFactsCore.Domain.Integrations.Twilio;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CatFactsCore.Functions.Startup))]

namespace CatFactsCore.Functions
{
    public class Startup : FunctionsStartup
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddCosmosRepositories(
                Configuration["Cosmos:CosmosEndpointUrl"],
                Configuration["Cosmos:AuthorizationKey"],
                Configuration["Cosmos:DatabaseId"],
                Configuration["Cosmos:ContainerId"]
            );

            builder.Services.AddDomain();

            builder.Services.AddTwilio(
                Configuration["Twilio:Sid"],
                Configuration["Twilio:Token"],
                Configuration["Twilio:FromNumber"]
            );

            builder.Services.AddCatFactNinja();
        }
    }
}