using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusServices.Application.DTO;
using TekusServices.Application.Interfaces;

namespace TekusServices.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProvidersController(IProviderService providerService) : ControllerBase
    {
        private readonly IProviderService _providerService = providerService;
        
        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            var providers = await _providerService.GetProvidersAsync();
            return Ok(providers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var provider = await _providerService.GetProviderByIdAsync(id);

            if (provider == null) return NotFound(new { Message = "Validate data" });

            return Ok(provider);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderCreateDTO providerCreateDTO)
        {
            try
            {
                bool existsProvider = await _providerService.ExistsProviderNitAsync(providerCreateDTO.Nit);

                if (existsProvider) return Conflict(new { Message = "Validate data" });

                int result = await _providerService.CreateProviderAsync(providerCreateDTO);

                if (result == 0) return StatusCode(500, new { Message = "Failed Creating Provider" });

                //return Ok();
                return Ok(new { Message = "Provider created successfully" });
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed Creating Provider" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            try
            {
                bool existsProvider = await _providerService.ExistsProviderIdAsync(id);

                if (!existsProvider) return NotFound(new { Message = "Validate data" });

                int result = await _providerService.DeleteProviderAsync(id);

                if (result == 0) return StatusCode(500, new { Message = "Failed to delete provider" });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ProviderUpdateDTO providerUpdateDTO)
        {
            try
            {
                if (id != providerUpdateDTO.ProviderId) return BadRequest("The ids are different");

                var result = await _providerService.UpdateProviderAsync(providerUpdateDTO);

                if (result == 0) return NotFound(new { Message = "Validate data" });

                return NoContent();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed updating provider" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }
    }
}
