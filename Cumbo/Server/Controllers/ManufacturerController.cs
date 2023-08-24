using Cumbo.Server.Models;
using Cumbo.Server.Services.ManufacturerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cumbo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturer;

        public ManufacturerController(IManufacturerService manufacturer)
        {
            _manufacturer = manufacturer;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _manufacturer.GetAllManufacturers();

            if (result is null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _manufacturer.GetManufacturer(id);

            if (result is null)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Manufacturer manufacturer)
        {
            var result = await _manufacturer.AddManufacturer(manufacturer);

            if (!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Manufacturer manufacturer)
        {
            var result = await _manufacturer.UpdateManufacturer(manufacturer);

            if (!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manufacturer.DeleteManufacturer(id);

            if (!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }
    }
}
