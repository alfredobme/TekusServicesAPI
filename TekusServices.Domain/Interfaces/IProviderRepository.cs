using TekusServices.Domain.Entities;

namespace TekusServices.Domain.Interfaces
{
    public interface IProviderRepository
    {
        Task<IEnumerable<Provider>> GetProviders();
        Task<Provider> GetProviderById(int id);
        Task<bool> ExistsProviderId(int id);
        Task<bool> ExistsProviderNit(string nit);
        Task<int> CreateProvider(Provider provider);
        Task<int> DeleteProvider(int id);
        Task<int> UpdateProvider(Provider provider);
    }
}
