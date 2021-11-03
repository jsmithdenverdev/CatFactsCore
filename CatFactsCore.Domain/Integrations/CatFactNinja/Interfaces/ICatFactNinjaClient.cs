using System.Threading.Tasks;
using CatFactsCore.Domain.Integrations.CatFactNinja.BindingModels;

namespace CatFactsCore.Domain.Integrations.CatFactNinja.Interfaces
{
    public interface ICatFactNinjaClient
    {
        public Task<GetRandomFactResponse> GetRandomFact();
    }
}