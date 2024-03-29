using System.Threading.Tasks;
using CatFactsCore.Domain.Commands.SendFact;
using MediatR;
using Microsoft.Azure.Functions.Worker;

namespace CatFactsCore.Functions
{
    public class DistributeFact
    {
        private readonly IMediator _mediator;

        public DistributeFact(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function("DistributeFact")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] DistributeFactTimerEvent timerEvent,
            FunctionContext context)
        {
            await _mediator.Send(new SendFactCommand());
        }
    }

    public class DistributeFactTimerEvent
    {
    }
}