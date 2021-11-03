using CatFactsCore.Domain.Integrations.CatFactNinja.Clients;
using CatFactsCore.Domain.Integrations.CatFactNinja.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactsCore.Domain.Integrations.CatFactNinja
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatFactNinja(this IServiceCollection services)
        {
            services.AddSingleton<ICatFactNinjaClient, CatFactNinjaClient>();

            return services;
        }
    }
}