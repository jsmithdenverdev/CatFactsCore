using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatFactsCore.Domain.Entities;

namespace CatFactsCore.Domain.Interfaces
{
    public interface ISubscriberRepository
    {
        public Task Write(Subscriber subscriber);
        public Task Delete(string contact);
        public Task<IEnumerable<Subscriber>> List();
    }
}