using System.Threading.Tasks;
using CatFactsCore.Domain.Integrations.Twilio.Interfaces;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Models;

namespace CatFactsCore.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ISmsService _smsService;

        public NotificationService(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public async Task SendSms(Sms sms)
        {
            await _smsService.SendSms(new Integrations.Twilio.Models.Sms
            {
                To = sms.To,
                Body = sms.Body
            });
        }
    }
}