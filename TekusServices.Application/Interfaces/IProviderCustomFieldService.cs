using System;
using System.Collections.Generic;
using System.Linq;
using TekusServices.Application.DTO;
using TekusServices.Domain.Entities;

namespace TekusServices.Application.Interfaces
{
    public interface IProviderCustomFieldService
    {
        Task<IEnumerable<ProviderCustomFieldCreateDTO>> GetProviderCustomFieldsAsync();
        Task<ProviderCustomFieldCreateDTO> GetProviderCustomFieldByIdAsync(int id);
        Task<bool> ExistsProviderCustomFieldIdAsync(int id);
        Task<int> CreateProviderCustomFieldAsync(ProviderCustomFieldCreateDTO providerCustomFieldCreateDTO);
        Task<int> DeleteProviderCustomFieldAsync(int id);
        Task<int> UpdateProviderCustomFieldAsync(ProviderCustomFieldUpdateDTO providerCustomFieldUpdateDTO);
    }
}
