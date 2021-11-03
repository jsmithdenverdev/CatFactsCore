using Newtonsoft.Json;

namespace CatFactsCore.Domain.Integrations.CatFactNinja.BindingModels
{
    public class GetRandomFactResponse
    {
        [JsonProperty("fact")] public string Fact { get; set; }
    }
}