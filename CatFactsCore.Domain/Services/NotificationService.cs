using System.Threading.Tasks;
using CatFactsCore.Domain.Exceptions;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Models;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CatFactsCore.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly string _from;

        public NotificationService(string from)
        {
            _from = from;
        }

        public async Task SendSms(Sms sms)
        {
            await MessageResource.CreateAsync(
                new PhoneNumber(sms.To),
                from: new PhoneNumber(_from),
                body: sms.Body
            );
        }
    }
}