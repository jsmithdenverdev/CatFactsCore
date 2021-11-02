using System;
using CatFactsCore.Domain.Enums;

namespace CatFactsCore.Domain.Entities
{
    public class Subscriber
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}