using TekusServices.Domain.Entities;

namespace TekusServices.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
