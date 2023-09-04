using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Services.HangfireService;
using Cumbo.Server.Services.ScrapeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cumbo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapeController : ControllerBase
    {
        private readonly IHangfireService _Scrape;
        public ScrapeController(IHangfireService scrape)
        {
            _Scrape = scrape;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            await _Scrape.SyncAds();

            return Ok();
        }
    }
}
