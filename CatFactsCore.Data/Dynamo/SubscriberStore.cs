using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CatFactsCore.Domain.Entities;
using CatFactsCore.Domain.Interfaces;

namespace CatFactsCore.Data.Dynamo
{
    public class SubscriberStore : ISubscriberStore
    {
        private readonly string _tableName;
        private readonly AmazonDynamoDBClient _client;

        public SubscriberStore(string tableName, AmazonDynamoDBClient client)
        {
            _tableName = tableName;
            _client = client;
        }

        public async Task Write(Subscriber subscriber)
        {
            await _client.PutItemAsync(new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {
                        "contact", new AttributeValue
                        {
                            S = subscriber.Contact
                        }
                    }
                }
            });
        }

        public async Task Delete(string contact)
        {
            await _client.DeleteItemAsync(new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {
                        "contact", new AttributeValue
                        {
                            S = contact
                        }
                    }
                }
            });
        }

        public async Task<IEnumerable<Subscriber>> List()
        {
            var result = await _client.ScanAsync(new ScanRequest
            {
                TableName = _tableName,
            });

            return result.Items.Select(item => new Subscriber
            {
                Contact = item["contact"].S
            });
        }
    }
}