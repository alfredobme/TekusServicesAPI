using Microsoft.AspNetCore.Mvc;
using TekusServices.Domain.Interfaces;

namespace TekusServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController(ICountryService countryService) : ControllerBase
    {
        private readonly ICountryService _countryService = countryService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }
    }
}
