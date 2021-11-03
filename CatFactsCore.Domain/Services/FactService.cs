using System.Threading.Tasks;
using CatFactsCore.Domain.Integrations.CatFactNinja.Interfaces;
using CatFactsCore.Domain.Interfaces;

namespace CatFactsCore.Domain.Services
{
    public class FactService : IFactService
    {
        private readonly ICatFactNinjaClient _client;

        public FactService(ICatFactNinjaClient client)
        {
            _client = client;
        }

        public async Task<string> RetrieveRandomFactAsync()
        {
            var result = await _client.GetRandomFact();
            return result.Fact;
        }
    }
}