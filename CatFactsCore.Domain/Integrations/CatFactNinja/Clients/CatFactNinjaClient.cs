using System.Net.Http;
using System.Threading.Tasks;
using CatFactsCore.Domain.Integrations.CatFactNinja.BindingModels;
using CatFactsCore.Domain.Integrations.CatFactNinja.Interfaces;
using Newtonsoft.Json;

namespace CatFactsCore.Domain.Integrations.CatFactNinja.Clients
{
    public class CatFactNinjaClient : ICatFactNinjaClient
    {
        private readonly HttpClient _client;

        public CatFactNinjaClient()
        {
            _client = new HttpClient();
        }

        public async Task<GetRandomFactResponse> GetRandomFact()
        {
            var result = await _client.GetAsync("/fact");
            var body = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GetRandomFactResponse>(body);
        }
    }
}