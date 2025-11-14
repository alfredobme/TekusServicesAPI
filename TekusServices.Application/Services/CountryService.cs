using System.Net.Http.Json;
using TekusServices.Domain.Entities;
using TekusServices.Domain.Interfaces;

namespace TekusServices.Application.Services
{
    public class CountryService(HttpClient httpClient) : ICountryService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            var url = "https://restcountries.com/v3.1/all?fields=name,cca2";
            var countries = await _httpClient.GetFromJsonAsync<List<RestCountry>>(url);

            return countries?.Select(c => new Country
            {
                Code = c.Cca2,
                Name = c.Name.Common
            }) ?? [];
        }

        private class RestCountry
        {
            public required string Cca2 { get; set; }
            public required NameInfo Name { get; set; }
        }

        private class NameInfo
        {
            public required string Common { get; set; }
        }
    }
}
