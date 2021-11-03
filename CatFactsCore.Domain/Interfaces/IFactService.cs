using System.Threading.Tasks;

namespace CatFactsCore.Domain.Interfaces
{
    public interface IFactService
    {
        public Task<string> RetrieveRandomFactAsync();
    }
}