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
    public class ProviderCustomFieldsController(IProviderCustomFieldService providerCustomFieldService) : ControllerBase
    {
        private readonly IProviderCustomFieldService _providerCustomFieldService = providerCustomFieldService;

        [HttpGet]
        public async Task<IActionResult> GetProviderCustomFields()
        {
            var providerCustomFields = await _providerCustomFieldService.GetProviderCustomFieldsAsync();
            return Ok(providerCustomFields);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProviderCustomFieldById(int id)
        {
            var providerCustomField = await _providerCustomFieldService.GetProviderCustomFieldByIdAsync(id);

            if (providerCustomField == null) return NotFound(new { Message = "Validate data" });

            return Ok(providerCustomField);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProviderCustomField([FromBody] ProviderCustomFieldCreateDTO providerCustomFieldDTO)
        {
            try
            {
                int result = await _providerCustomFieldService.CreateProviderCustomFieldAsync(providerCustomFieldDTO);

                if (result == 0) return StatusCode(500, new { Message = "Failed creating provider custom field" });

                //return Ok();
                return Ok(new { Message = "Provider custom field created successfully" });
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed creating provider custom field" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProviderCustomField(int id)
        {
            try
            {
                bool existsProviderCustomField = await _providerCustomFieldService.ExistsProviderCustomFieldIdAsync(id);

                if (!existsProviderCustomField) return NotFound(new { Message = "Validate data" });

                int result = await _providerCustomFieldService.DeleteProviderCustomFieldAsync(id);

                if (result == 0) return StatusCode(500, new { Message = "Failed to delete provider custom field" });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ProviderCustomFieldUpdateDTO providerCustomFieldUpdateDTO)
        {
            try
            {
                if (id != providerCustomFieldUpdateDTO.ProviderCustomFieldId) return BadRequest("The ids are different");

                var result = await _providerCustomFieldService.UpdateProviderCustomFieldAsync(providerCustomFieldUpdateDTO);

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
