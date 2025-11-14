using Microsoft.EntityFrameworkCore;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Infrastructure.Data.Repositories
{
    public class ProviderRepository(ApplicationDbContext dbContext) : IProviderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<IEnumerable<Provider>> GetProviders()
        {
            // Para excluir los inactivos
            // return await _dbContext.Providers
            //     .Where(s => s.Active)
            //     .Include(p => p.Services.Where(s => s.Active))
            //     .ToListAsync();

            return await _dbContext.Providers
                .Include(p => p.Services)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        public async Task<Provider> GetProviderById(int id)
        {
            var provider = await _dbContext.Providers
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.ProviderId == id);

            return provider!;
        }
        public async Task<int> CreateProvider(Provider provider)
        {
            _dbContext.Providers.Add(provider);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsProviderId(int id)
        {
            return await _dbContext.Providers.AnyAsync(p => p.ProviderId == id);
        }
        public async Task<bool> ExistsProviderNit(string nit)
        {
            return await _dbContext.Providers.AnyAsync(p => p.Nit == nit);
        }
        public async Task<int> DeleteProvider(int id)
        {
            return await _dbContext.Providers.Where(p => p.ProviderId == id).ExecuteDeleteAsync();
        }
        public async Task<int> UpdateProvider(Provider provider)
        {
            var existingProvider = await _dbContext.Providers.FindAsync(provider.ProviderId);

            if (existingProvider == null) return 0;

            existingProvider.Nit = provider.Nit;
            existingProvider.Name = provider.Name;
            existingProvider.Email = provider.Email;
            existingProvider.Active = provider.Active;

            _dbContext.Providers.Update(existingProvider);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
