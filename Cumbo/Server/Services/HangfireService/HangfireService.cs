using AutoMapper;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Repository;
using Cumbo.Server.Services.ScrapeService;

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

            HashSet<string> queryedTitles = new HashSet<string>(queryedAds.Select(ad => ad.Title));

            List<Advertisment> unmatchedAds = new();
            List<Advertisment> updatedAds = new();

            foreach (var scrapedAd in scrapedAds)
            {
                if (queryedTitles.Contains(scrapedAd.Title))
                {
                    var existingAd = queryedAds.First(ad => ad.Title == scrapedAd.Title);

                    if (existingAd.Url != scrapedAd.Url)
                    {
                        existingAd.Url = scrapedAd.Url;
                        updatedAds.Add(existingAd);
                    }
                }
                else
                {
                    unmatchedAds.Add(scrapedAd);
                }
            }

            if (updatedAds.Count > 0)
            {
                await _unitOfWork.Advertisement.UpdateRange(updatedAds);
            }

            if (unmatchedAds.Count > 0)
            {
                await _unitOfWork.Advertisement.AddRange(unmatchedAds);
            }

            if (updatedAds.Count > 0 || unmatchedAds.Count > 0)
            {
                await _unitOfWork.Save();
                Console.WriteLine($"Saved {updatedAds.Count + unmatchedAds.Count} ads to the database");
            }

        }
    }
}
