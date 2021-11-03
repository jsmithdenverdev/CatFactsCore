using System.Threading.Tasks;
using CatFactsCore.Domain.Integrations.Twilio.Models;

namespace CatFactsCore.Domain.Integrations.Twilio.Interfaces
{
    public interface ISmsService
    {
        public Task SendSms(Sms sms);
    }
}