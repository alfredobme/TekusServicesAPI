using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekusServices.Application.Interfaces;

namespace TekusServices.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class IndicatorsController(IServiceService serviceService) : ControllerBase
    {
        private readonly IServiceService _serviceService = serviceService;

        [HttpGet("ServicesByCountry")]
        public async Task<IActionResult> GetServicesByCountry()
        {
            var indicators = await _serviceService.GetServicesByCountryAsync();
            return Ok(indicators);
        }

        [HttpGet("ProvidersByCountry")]
        public async Task<IActionResult> GetProvidersByCountry()
        {
            var indicators = await _serviceService.GetProvidersByCountryAsync();
            return Ok(indicators);
        }
    }
}
