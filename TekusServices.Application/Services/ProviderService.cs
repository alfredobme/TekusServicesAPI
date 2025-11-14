using AutoMapper;
using TekusServices.Application.DTO;
using TekusServices.Application.Interfaces;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Application.Services
{
    public class ProviderService(IMapper mapper, IProviderRepository providerRepository) : IProviderService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProviderRepository _providerRepository = providerRepository;

        public async Task<IEnumerable<ProviderDTO>> GetProvidersAsync()
        {
            var providers = await _providerRepository.GetProviders();

            return _mapper.Map<IEnumerable<ProviderDTO>>(providers);
        }

        public async Task<ProviderDTO> GetProviderByIdAsync(int id)
        {
            var provider = await _providerRepository.GetProviderById(id);

            return _mapper.Map<ProviderDTO>(provider);
        }
        public async Task<bool> ExistsProviderIdAsync(int id)
        {
            return await _providerRepository.ExistsProviderId(id);
        }
        public async Task<bool> ExistsProviderNitAsync(string nit)
        {
            return await _providerRepository.ExistsProviderNit(nit);
        }
        public async Task<int> CreateProviderAsync(ProviderCreateDTO providerCreateDTO)
        {
            var provider = _mapper.Map<Provider>(providerCreateDTO);

            return await _providerRepository.CreateProvider(provider);
        }
        public async Task<int> DeleteProviderAsync(int id)
        {
            return await _providerRepository.DeleteProvider(id);
        }
        public async Task<int> UpdateProviderAsync(ProviderUpdateDTO providerUpdateDTO)
        {
            var provider = _mapper.Map<Provider>(providerUpdateDTO);

            return await _providerRepository.UpdateProvider(provider);
        }
    }
}
