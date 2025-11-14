using Microsoft.EntityFrameworkCore;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Infrastructure.Data.Repositories
{
    public class ProviderCustomFieldRepository(ApplicationDbContext dbContext) : IProviderCustomFieldRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<IEnumerable<ProviderCustomField>> GetProviderCustomFields()
        {
            return await _dbContext.ProviderCustomFields
                .ToListAsync();
        }
        public async Task<ProviderCustomField> GetProviderCustomFieldById(int id)
        {
            var providerCustomField = await _dbContext.ProviderCustomFields
                .FirstOrDefaultAsync(p => p.ProviderCustomFieldId == id);

            return providerCustomField!;
        }
        public async Task<int> CreateProviderCustomField(ProviderCustomField providerCustomField)
        {
            _dbContext.ProviderCustomFields.Add(providerCustomField);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsProviderCustomFieldId(int id)
        {
            return await _dbContext.ProviderCustomFields.AnyAsync(p => p.ProviderCustomFieldId == id);
        }
        public async Task<int> DeleteProviderCustomField(int id)
        {
            return await _dbContext.ProviderCustomFields.Where(p => p.ProviderCustomFieldId == id).ExecuteDeleteAsync();
        }
        public async Task<int> UpdateProviderCustomField(ProviderCustomField providerCustomField)
        {
            var existingProviderCustomField = await _dbContext.ProviderCustomFields.FindAsync(providerCustomField.ProviderCustomFieldId);

            if (existingProviderCustomField == null) return 0;

            existingProviderCustomField.ProviderId = providerCustomField.ProviderId;
            existingProviderCustomField.FieldName = providerCustomField.FieldName;
            existingProviderCustomField.FieldValue = providerCustomField.FieldValue;

            _dbContext.ProviderCustomFields.Update(existingProviderCustomField);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
