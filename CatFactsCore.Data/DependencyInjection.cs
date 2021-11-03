using System.Reflection;
using Amazon.DynamoDBv2;
using CatFactsCore.Data.Repositories;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Services;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactsCore.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCosmosRepositories(
            this IServiceCollection services,
            string endpointUrl,
            string authorizationKey,
            string databaseId,
            string containerId)
        {
            // Cosmos
            services.AddSingleton(s => new CosmosClient(endpointUrl, authorizationKey));

            // Repositories
            services.AddTransient(s =>
                new SubscriberRepository(databaseId, containerId, s.GetService<CosmosClient>()));

            return services;
        }
    }
}