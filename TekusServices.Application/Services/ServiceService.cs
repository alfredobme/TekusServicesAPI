using AutoMapper;
using System.Text.Json;
using TekusServices.Application.DTO;
using TekusServices.Application.Interfaces;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Application.Services
{
    public class ServiceService(IMapper mapper, IServiceRepository serviceRepository) : IServiceService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IServiceRepository _serviceRepository = serviceRepository;

        public async Task<IEnumerable<ServiceDTO>> GetServicesAsync()
        {
            var services = await _serviceRepository.GetServices();

            return _mapper.Map<IEnumerable<ServiceDTO>>(services);
        }

        public async Task<ServiceDTO> GetServiceByIdAsync(int id)
        {
            var service = await _serviceRepository.GetServiceById(id);

            return _mapper.Map<ServiceDTO>(service);
        }
        public async Task<bool> ExistsServiceIdAsync(int id)
        {
            return await _serviceRepository.ExistsServiceId(id);
        }
        public async Task<int> CreateServiceAsync(ServiceCreateDTO serviceCreateDTO)
        {
            var service = _mapper.Map<Service>(serviceCreateDTO);

            return await _serviceRepository.CreateService(service);
        }
        public async Task<int> DeleteServiceAsync(int id)
        {
            return await _serviceRepository.DeleteService(id);
        }
        public async Task<int> UpdateServiceAsync(ServiceUpdateDTO serviceUpdateDTO)
        {
            var service = _mapper.Map<Service>(serviceUpdateDTO);

            return await _serviceRepository.UpdateService(service);
        }
        public async Task<IEnumerable<IndicatorServiceDTO>> GetServicesByCountryAsync()
        {
            var services = await _serviceRepository.GetServices();

            var countryCounts = new Dictionary<string, int>();

            foreach (var service in services.Where(s => s.Active))
            {
                if (string.IsNullOrEmpty(service.CountriesJson))
                    continue;

                try
                {
                    var countries = JsonSerializer.Deserialize<List<CountryJson>>(service.CountriesJson) ?? new();

                    foreach (var country in countries)
                    {
                        if (string.IsNullOrWhiteSpace(country.Name)) continue;

                        if (countryCounts.ContainsKey(country.Name))
                            countryCounts[country.Name]++;
                        else
                            countryCounts[country.Name] = 1;
                    }
                }
                catch
                {
                }
            }

            return countryCounts.Select(c => new IndicatorServiceDTO
            {
                Country = c.Key,
                ActiveServices = c.Value
            });
        }

        public async Task<IEnumerable<IndicatorProviderDTO>> GetProvidersByCountryAsync()
        {
            var services = await _serviceRepository.GetServices();

            var countryProviders = new Dictionary<string, HashSet<int>>();

            foreach (var service in services.Where(s => s.Active))
            {
                if (string.IsNullOrEmpty(service.CountriesJson))
                    continue;

                try
                {
                    var countries = JsonSerializer.Deserialize<List<CountryJson>>(service.CountriesJson) ?? new();

                    foreach (var country in countries)
                    {
                        if (string.IsNullOrWhiteSpace(country.Name) || country.Name.Equals("string", StringComparison.OrdinalIgnoreCase))
                            continue;

                        if (!countryProviders.ContainsKey(country.Name))
                            countryProviders[country.Name] = new HashSet<int>();

                        countryProviders[country.Name].Add(service.ProviderId);
                    }
                }
                catch
                {
                }
            }

            return countryProviders.Select(c => new IndicatorProviderDTO
            {
                Country = c.Key,
                Providers = c.Value.Count
            });
        }

        private class CountryJson
        {
            public string Code { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}

