using System.Reflection;
using Amazon.DynamoDBv2;
using CatFactsCore.Data.Dynamo;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactsCore.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDynamoStorage(
            this IServiceCollection services,
            string tableName)

        {
            services.AddTransient<AmazonDynamoDBClient>();

            services.AddTransient<ISubscriberStore, SubscriberStore>(s =>
                new SubscriberStore(tableName, s.GetService<AmazonDynamoDBClient>()));

            return services;
        }
    }
}