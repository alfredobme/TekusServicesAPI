using Microsoft.EntityFrameworkCore;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Infrastructure.Data.Repositories
{
    public class ServiceRepository(ApplicationDbContext dbContext) : IServiceRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _dbContext.Services
                .Include(s => s.Provider)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
        public async Task<Service> GetServiceById(int id)
        {
            var service = await _dbContext.Services
                .FirstOrDefaultAsync(p => p.ServiceId == id);

            return service!;
        }
        public async Task<int> CreateService(Service service)
        {
            _dbContext.Services.Add(service);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsServiceId(int id)
        {
            return await _dbContext.Services.AnyAsync(p => p.ServiceId == id);
        }
        public async Task<int> DeleteService(int id)
        {
            return await _dbContext.Services.Where(p => p.ServiceId == id).ExecuteDeleteAsync();
        }
        public async Task<int> UpdateService(Service service)
        {
            var existingService = await _dbContext.Services.FindAsync(service.ServiceId);

            if (existingService == null) return 0;

            existingService.Name = service.Name;
            existingService.HourlyRate = service.HourlyRate;
            existingService.Active = service.Active;
            existingService.ProviderId = service.ProviderId;
            existingService.CountriesJson = service.CountriesJson;

            _dbContext.Services.Update(existingService);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
