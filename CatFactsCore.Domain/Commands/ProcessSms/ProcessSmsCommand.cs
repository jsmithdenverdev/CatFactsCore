using CatFactsCore.Domain.Models;
using MediatR;

namespace CatFactsCore.Domain.Commands.ProcessSms
{
    public class ProcessSmsCommand : IRequest
    {
        public Sms Sms { get; set; }
    }
}