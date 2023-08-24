using Cumbo.Server.Models.KupujemProdajemAd;

namespace Cumbo.Server.Services.ScrapeService
{
    public interface IScrapeService
    {
        public List<Advertisment> ScrapeAdsPerPage(int page);
    }
}
