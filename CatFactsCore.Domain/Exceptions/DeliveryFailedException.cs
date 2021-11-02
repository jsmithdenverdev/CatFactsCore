using System;

namespace CatFactsCore.Domain.Exceptions
{
    public class DeliveryFailedException : Exception
    {
        public DeliveryFailedException(string contact) : base($"Delivery of message to {contact} failed")
        {
        }
    }
}