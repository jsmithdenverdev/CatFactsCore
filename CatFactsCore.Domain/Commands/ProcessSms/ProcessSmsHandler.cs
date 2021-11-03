using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CatFactsCore.Domain.Constants;
using CatFactsCore.Domain.Entities;
using CatFactsCore.Domain.Enums;
using CatFactsCore.Domain.Exceptions;
using CatFactsCore.Domain.Interfaces;
using CatFactsCore.Domain.Models;
using MediatR;

namespace CatFactsCore.Domain.Commands.ProcessSms
{
    public class ProcessSmsHandler : IRequestHandler<ProcessSmsCommand>
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly INotificationService _notificationService;

        private static readonly List<Tuple<SmsOperation, string[]>> Operations =
            new List<Tuple<SmsOperation, string[]>>()
            {
                new Tuple<SmsOperation, string[]>(
                    SmsOperation.Subscribe,
                    new[] {"start", "yes", "unstop"}),
                new Tuple<SmsOperation, string[]>(
                    SmsOperation.Unsubscribe,
                    new[] {"stop", "stopall", "unsubscribe", "cancel", "end", "quit"}),
                new Tuple<SmsOperation, string[]>(
                    SmsOperation.Help,
                    new[] {"help", "info"}),
                new Tuple<SmsOperation, string[]>(
                    SmsOperation.Cuss,
                    new[] {"shit", "fuck", "damn", "cuss", "swear", "hec", "heck"}),
            };

        public ProcessSmsHandler(ISubscriberRepository subscriberRepository, INotificationService notificationService)
        {
            _subscriberRepository = subscriberRepository;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(ProcessSmsCommand request, CancellationToken cancellationToken)
        {
            SmsOperation operation;

            try
            {
                operation = GetOperationFromBody(request.Sms.Body);
            }
            catch (ArgumentNullException)
            {
                throw new InvalidCommandException(request.Sms.Body);
            }

            try
            {
                switch (operation)
                {
                    case SmsOperation.Subscribe:
                        await Subscribe(request.Sms.From);
                        break;
                    case SmsOperation.Unsubscribe:
                        await Unsubscribe(request.Sms.From);
                        break;
                    case SmsOperation.Help:
                        await Help(request.Sms.From);
                        break;
                    default:
                        await Cuss(request.Sms.From);
                        break;
                }
            }
            catch (Exception)
            {
                await _notificationService.SendSms(
                    new Sms
                    {
                        To = request.Sms.From, Body = SmsReplies.Error
                    });
            }

            return Unit.Value;
        }


        private async Task Subscribe(string contact)
        {
            await _subscriberRepository.Write(new Subscriber
            {
                Contact = contact
            });

            await _notificationService.SendSms(
                new Sms
                {
                    To = contact, Body = SmsReplies.Welcome
                });
        }

        private async Task Unsubscribe(string contact)
        {
            await _subscriberRepository.Delete(contact);
            await _notificationService.SendSms(
                new Sms
                {
                    To = contact, Body = SmsReplies.Goodbye
                });
        }

        private async Task Help(string contact)
        {
            await _notificationService.SendSms(
                new Sms
                {
                    To = contact, Body = SmsReplies.Help
                });
        }

        /// <summary>
        /// Heck
        /// </summary>
        /// <param name="contact"></param>
        private async Task Cuss(string contact)
        {
            await _notificationService.SendSms(
                new Sms
                {
                    To = contact, Body = SmsReplies.Cuss
                });
        }

        private static SmsOperation GetOperationFromBody(string body)
        {
            body = body.ToLowerInvariant();

            var operation = Operations
                .Single(operation => operation.Item2.Contains(body));

            return operation.Item1;
        }
    }
}