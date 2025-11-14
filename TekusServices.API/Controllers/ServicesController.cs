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
    public class ServicesController(IServiceService serviceService) : ControllerBase
    {
        private readonly IServiceService _serviceService = serviceService;

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await _serviceService.GetServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

            if (service == null) return NotFound(new { Message = "Validate data" });

            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceCreateDTO serviceCreateDTO)
        {
            try
            {
                int result = await _serviceService.CreateServiceAsync(serviceCreateDTO);

                if (result == 0) return StatusCode(500, new { Message = "Failed creating service" });

                //return Ok();
                return Ok(new { Message = "Service created successfully" });
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed creating service" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                bool existsService = await _serviceService.ExistsServiceIdAsync(id);

                if (!existsService) return NotFound(new { Message = "Validate data" });

                int result = await _serviceService.DeleteServiceAsync(id);

                if (result == 0) return StatusCode(500, new { Message = "Failed to delete service" });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ServiceUpdateDTO serviceUpdateDTO)
        {
            try
            {
                if (id != serviceUpdateDTO.ServiceId) return BadRequest("The ids are different");

                var result = await _serviceService.UpdateServiceAsync(serviceUpdateDTO);

                if (result == 0) return NotFound(new { Message = "Validate data" });

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed updating service" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal error" });
            }
        }

    }
}
