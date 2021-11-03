using System.Threading;
using System.Threading.Tasks;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Models;
using MediatR;

namespace CatFactsCore.Domain.Commands.SendFact
{
    public class SendFactHandler : IRequestHandler<SendFactCommand>
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IFactService _factService;
        private readonly INotificationService _notificationService;

        public SendFactHandler(
            ISubscriberRepository subscriberRepository,
            IFactService factService,
            INotificationService notificationService)
        {
            _subscriberRepository = subscriberRepository;
            _factService = factService;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(SendFactCommand request, CancellationToken cancellationToken)
        {
            var fact = await _factService.RetrieveRandomFactAsync();
            var subscribers = await _subscriberRepository.List();

            foreach (var subscriber in subscribers)
            {
                await _notificationService.SendSms(new Sms
                {
                    Body = fact,
                    To = subscriber.Contact
                });
            }

            return Unit.Value;
        }
    }
}