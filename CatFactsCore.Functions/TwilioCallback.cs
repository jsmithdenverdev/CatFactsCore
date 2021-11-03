using System.Net;
using System.Threading.Tasks;
using CatFactsCore.Domain.Commands.ProcessSms;
using CatFactsCore.Domain.Models;
using HttpMultipartParser;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace CatFactsCore.Functions
{
    public class TwilioCallback
    {
        private readonly IMediator _mediator;

        public TwilioCallback(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function("TwilioCallback")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var parsedForm = await MultipartFormDataParser.ParseAsync(req.Body);
            var from = parsedForm.GetParameterValue("From");
            var body = parsedForm.GetParameterValue("Body");

            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(body))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            await _mediator.Send(new ProcessSmsCommand
            {
                Sms = new Sms
                {
                    From = from,
                    Body = body
                }
            });

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}