using System.Threading.Tasks;
using CatFactsCore.Domain.Integrations.Twilio.Interfaces;
using CatFactsCore.Domain.Integrations.Twilio.Models;
using Twilio.Rest.Api.V2010.Account;

namespace CatFactsCore.Domain.Integrations.Twilio.Services
{
    public class SmsService : ISmsService
    {
        private readonly string _from;

        public SmsService(string from)
        {
            _from = from;
        }

        public async Task SendSms(Sms sms)
        {
            await MessageResource.CreateAsync(
                to: sms.To,
                from: _from,
                body: sms.Body);
        }
    }
}