using System.Reflection;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CatFactsCore.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommandsAndQueries(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddNotificationService(
            this IServiceCollection services,
            string sid,
            string token,
            string fromNumber)
        {
            // This method should only be called once.
            Twilio.TwilioClient.Init(sid, token);

            services.AddSingleton<INotificationService>(new NotificationService(fromNumber));

            return services;
        }
    }
}