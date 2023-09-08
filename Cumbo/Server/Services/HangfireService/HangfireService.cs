using AutoMapper;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Repository;
using Cumbo.Server.Services.ScrapeService;
using Cumbo.Shared.DTOs.Advertisement;

namespace Cumbo.Server.Services.HangfireService
{
    public class HangfireService : IHangfireService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScrapeService _scrapeService;
        private readonly IMapper _mapper;
        public HangfireService(IUnitOfWork unitOfWork,
                               IScrapeService scrapeService,
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _scrapeService = scrapeService;
            _mapper = mapper;
        }

        public async Task SyncAds()
        {
            var scraped = await _scrapeService.ScrapeAds();

            if (!scraped.Success)
                throw new Exception(scraped.Message);

            List<Advertisment> scrapedAds = _mapper.Map<List<Advertisment>>(scraped.Data);
            List<Advertisment> queryedAds = await _unitOfWork.Advertisement.GetActive();

            var queryedAdsByTitle = queryedAds.ToDictionary(ad => ad.Title);

            List<Advertisment> updatedAds = new();
            List<Advertisment> newAds = new();

            // Iterate through scrapedAds to update LastActive and match URLs
            foreach (var scrapedAd in scrapedAds)
            {
                if (queryedAdsByTitle.TryGetValue(scrapedAd.Title, out var queryedAd))
                {
                    // Update LastActive
                    queryedAd.LastActive = DateTime.UtcNow;

                    // Update URL if it's different
                    if (queryedAd.Url != scrapedAd.Url)
                    {
                        queryedAd.Url = scrapedAd.Url;
                        updatedAds.Add(queryedAd);
                    }

                    // Remove the matched queryedAd
                    queryedAdsByTitle.Remove(scrapedAd.Title);
                }
                else
                {
                    // Add unmatched scrapedAd to newAds
                    newAds.Add(scrapedAd);
                }
            }

            // Deactivate unmatched queryedAds
            foreach (var queryedAd in queryedAdsByTitle.Values)
            {
                queryedAd.CurrentlyActive = false;
                updatedAds.Add(queryedAd);
            }

            // Update the modified queryedAds
            if (updatedAds.Count > 0)
            {
                await _unitOfWork.Advertisement.UpdateRange(updatedAds);
            }

            // Add new unmatched scrapedAds to the database
            if (newAds.Count > 0)
            {
                await _unitOfWork.Advertisement.AddRange(newAds);
            }

            // Save changes
            await _unitOfWork.Save();

            Console.WriteLine($"Sync completed. Updated: {updatedAds.Count}, Added: {newAds.Count}");

        }
    }
}
