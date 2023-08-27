using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Shared;

namespace Cumbo.Server.Services.ScrapeService
{
    public interface IScrapeService
    {
        Task<ServiceResponse<List<AdvertismentDto>>> ScrapeAds();
    }
}
