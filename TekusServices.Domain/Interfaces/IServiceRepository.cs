using TekusServices.Domain.Entities;

namespace TekusServices.Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServices();
        Task<Service> GetServiceById(int id);
        Task<bool> ExistsServiceId(int id);
        Task<int> CreateService(Service service);
        Task<int> DeleteService(int id);
        Task<int> UpdateService(Service service);
    }
}

