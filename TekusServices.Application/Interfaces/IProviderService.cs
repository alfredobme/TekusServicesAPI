using TekusServices.Application.DTO;

namespace TekusServices.Application.Interfaces
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderDTO>> GetProvidersAsync();
        Task<ProviderDTO> GetProviderByIdAsync(int id);
        Task<bool> ExistsProviderIdAsync(int id);
        Task<bool> ExistsProviderNitAsync(string nit);
        Task<int> CreateProviderAsync(ProviderCreateDTO providerCreateDTO);
        Task<int> DeleteProviderAsync(int id);
        Task<int> UpdateProviderAsync(ProviderUpdateDTO providerUpdateDTO);
    }
}
