using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CatFactsCore.Domain.Entities;
using CatFactsCore.Domain.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CatFactsCore.Data.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly string _databaseId;
        private readonly string _containerId;
        private readonly CosmosClient _client;

        public SubscriberRepository(string databaseId, string containerId, CosmosClient client)
        {
            _databaseId = databaseId;
            _containerId = containerId;
            _client = client;
        }

        public async Task Write(Subscriber subscriber)
        {
            var container = _client.GetContainer(_databaseId, _containerId);

            await container.CreateItemAsync(subscriber, new PartitionKey(subscriber.Contact));
        }

        public async Task Delete(string contact)
        {
            var container = _client.GetContainer(_databaseId, _containerId);

            await container.DeleteItemAsync<Subscriber>(contact, new PartitionKey(contact));
        }

        public async Task<IEnumerable<Subscriber>> List()
        {
            var subscribers = new List<Subscriber>();
            var container = _client.GetContainer(_databaseId, _containerId);
            var query = new QueryDefinition("SELECT * FROM c");

            using var feedIterator = container.GetItemQueryIterator<Subscriber>(query);

            while (feedIterator.HasMoreResults)
            {
                subscribers.AddRange(await feedIterator.ReadNextAsync());
            }

            return subscribers;
        }
    }
}