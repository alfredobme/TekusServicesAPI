using AutoMapper;
using TekusServices.Application.DTO;
using TekusServices.Application.Interfaces;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Application.Services
{
    public class ProviderCustomFieldService(IMapper mapper, IProviderCustomFieldRepository providerCustomFieldRepository) : IProviderCustomFieldService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProviderCustomFieldRepository _providerCustomFieldRepository = providerCustomFieldRepository;

        public async Task<IEnumerable<ProviderCustomFieldCreateDTO>> GetProviderCustomFieldsAsync()
        {
            var providerCustomFields = await _providerCustomFieldRepository.GetProviderCustomFields();

            return _mapper.Map<IEnumerable<ProviderCustomFieldCreateDTO>>(providerCustomFields);
        }

        public async Task<ProviderCustomFieldCreateDTO> GetProviderCustomFieldByIdAsync(int id)
        {
            var providerCustomField = await _providerCustomFieldRepository.GetProviderCustomFieldById(id);

            return _mapper.Map<ProviderCustomFieldCreateDTO>(providerCustomField);
        }
        public async Task<bool> ExistsProviderCustomFieldIdAsync(int id)
        {
            return await _providerCustomFieldRepository.ExistsProviderCustomFieldId(id);
        }
        public async Task<int> CreateProviderCustomFieldAsync(ProviderCustomFieldCreateDTO providerCustomFieldDTO)
        {
            var providerCustomField = _mapper.Map<ProviderCustomField>(providerCustomFieldDTO);

            return await _providerCustomFieldRepository.CreateProviderCustomField(providerCustomField);
        }
        public async Task<int> DeleteProviderCustomFieldAsync(int id)
        {
            return await _providerCustomFieldRepository.DeleteProviderCustomField(id);
        }
        public async Task<int> UpdateProviderCustomFieldAsync(ProviderCustomFieldUpdateDTO providerCustomFieldUpdateDTO)
        {
            var providerCustomField = _mapper.Map<ProviderCustomField>(providerCustomFieldUpdateDTO);

            return await _providerCustomFieldRepository.UpdateProviderCustomField(providerCustomField);
        }
    }
}
