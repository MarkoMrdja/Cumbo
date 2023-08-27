using Cumbo.Server.Models;
using Cumbo.Server.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cumbo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Manufacturer.GetAll();

            if (result is not null)
                return Ok(result);

            return BadRequest();    
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitOfWork.Manufacturer.GetById(id);

            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Manufacturer manufacturer)
        {
            var result = await _unitOfWork.Manufacturer.Add(manufacturer);

            if (result)
            {
                await _unitOfWork.Save();
                return Ok(result);
            }
            
            return BadRequest(result);    
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Manufacturer manufacturer)
        {
            var existing = await _unitOfWork.Manufacturer.GetById(manufacturer.Id);

            if (existing is null)
                return BadRequest();

            var result = await _unitOfWork.Manufacturer.Update(manufacturer);
            await _unitOfWork.Save();

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.Manufacturer.GetById(id);

            if (entity is null)
                return BadRequest();

            var result = await _unitOfWork.Manufacturer.Remove(entity);
            await _unitOfWork.Save();

            return Ok(result);
        }
    }
}
