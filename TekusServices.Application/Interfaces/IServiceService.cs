using TekusServices.Application.DTO;
using TekusServices.Domain.Entities;

namespace TekusServices.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDTO>> GetServicesAsync();
        Task<ServiceDTO> GetServiceByIdAsync(int id);
        Task<bool> ExistsServiceIdAsync(int id);
        Task<int> CreateServiceAsync(ServiceCreateDTO serviceCreateDTO);
        Task<int> DeleteServiceAsync(int id);
        Task<int> UpdateServiceAsync(ServiceUpdateDTO serviceUpdateDTO);
        Task<IEnumerable<IndicatorServiceDTO>> GetServicesByCountryAsync();
        Task<IEnumerable<IndicatorProviderDTO>> GetProvidersByCountryAsync();
    }
}
