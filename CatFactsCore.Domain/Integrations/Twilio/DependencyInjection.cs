using System;
using CatFactsCore.Domain.Integrations.Twilio.Interfaces;
using CatFactsCore.Domain.Integrations.Twilio.Services;
using Microsoft.Extensions.DependencyInjection;
using Twilio;

namespace CatFactsCore.Domain.Integrations.Twilio
{
    public static class DependencyInjection
    {
        private static bool _initialized;

        public static IServiceCollection AddTwilio(
            this IServiceCollection services,
            string accountSid,
            string accountToken,
            string fromNumber)
        {
            if (_initialized)
            {
                throw new Exception("Attempted to initialize TwilioClient more than once.");
            }

            TwilioClient.Init(accountSid, accountToken);

            services.AddSingleton<ISmsService>(s => new SmsService(fromNumber));

            _initialized = true;

            return services;
        }
    }
}