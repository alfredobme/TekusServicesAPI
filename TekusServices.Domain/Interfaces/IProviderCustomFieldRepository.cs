using TekusServices.Domain.Entities;

namespace TekusServices.Domain.Interfaces
{
    public interface IProviderCustomFieldRepository
    {
        Task<IEnumerable<ProviderCustomField>> GetProviderCustomFields();
        Task<ProviderCustomField> GetProviderCustomFieldById(int id);
        Task<bool> ExistsProviderCustomFieldId(int id);
        Task<int> CreateProviderCustomField(ProviderCustomField providerCustomField);
        Task<int> DeleteProviderCustomField(int id);
        Task<int> UpdateProviderCustomField(ProviderCustomField providerCustomField);
    }
}
