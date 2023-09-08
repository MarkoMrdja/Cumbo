using Cumbo.Server.Repository;
using Cumbo.Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cumbo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdvertismentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActive([FromQuery] AdvertisementParameters advertisementParameters)
        {
            var activeAds = await _unitOfWork.Advertisement.GetActivePaged(advertisementParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(activeAds.MetaData));

            return Ok(activeAds);
        }
    }
}
