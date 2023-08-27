using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Services.ScrapeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cumbo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapeController : ControllerBase
    {
        private readonly IScrapeService _Scrape;
        public ScrapeController(IScrapeService scrape)
        {
            _Scrape = scrape;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var result = await _Scrape.ScrapeAds();

            if (!result.Success)
                return BadRequest(result);
            else
                return Ok(result);
        }
    }
}
