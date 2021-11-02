using System.Threading.Tasks;
using CatFactsCore.Domain.Models;

namespace CatFactsCore.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendSms(Sms sms);
    }
}