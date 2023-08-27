using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Shared;
using HtmlAgilityPack;

namespace Cumbo.Server.Services.ScrapeService
{
    public class ScrapeService : IScrapeService
    {
        private readonly HtmlWeb _Web;

        public ScrapeService(HtmlWeb web)
        {
            _Web = web;
            _Web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36";

        }

        public async Task<ServiceResponse<List<AdvertismentDto>>> ScrapeAds()
        {
            ServiceResponse<List<AdvertismentDto>> scrapedAds = new();

            int limit = await GetPageLimit();

            try
            {
                scrapedAds.Data = await FetchAds(limit);
                
                if(scrapedAds.Data.Count is not 0) 
                {
                    scrapedAds.Success = true;
                    scrapedAds.Message = "Ads scraped successfully";
                }

                return scrapedAds;
            }
            catch (Exception ex)
            {
                scrapedAds.Success = false;
                scrapedAds.Message = $"There was an error while scraping ads: {ex.Message}";

                return scrapedAds;
            }
            
        }

        private async Task<int> GetPageLimit()
        {
            List<int> pageNumbers = new();

            var document = await _Web.LoadFromWebAsync("https://novi.kupujemprodajem.com/aleksandar-ns-0654389483/svi-oglasi/470651/1?order=price");

            var paginationElements = document.DocumentNode.QuerySelectorAll("a.Pagination_item__usZku");

            foreach(var pageButton in paginationElements)
            {
                var pageNumberText = HtmlEntity.DeEntitize(pageButton.GetAttributeValue("aria-label", ""));

                if (int.TryParse(pageNumberText, out int pageNumber))
                    pageNumbers.Add(pageNumber);
            }

            return pageNumbers.Max();
        }

        private async Task<List<AdvertismentDto>> FetchAds(int limit)
        {
            List<AdvertismentDto> advertisments = new();

            for (int i = 1; i <= limit; i++)
            {
                var document = await _Web.LoadFromWebAsync($"https://novi.kupujemprodajem.com/aleksandar-ns-0654389483/svi-oglasi/470651/{i}?order=price");

                var articleElements = document.DocumentNode.QuerySelectorAll("article.AdItem_adHolder__NoNLJ");

                foreach (var article in articleElements)
                {
                    var name = HtmlEntity.DeEntitize(article.QuerySelector(".AdItem_name__RhGAZ")?.InnerText);
                    var imageSrc = HtmlEntity.DeEntitize(article.QuerySelector(".AdItem_imageHolder__LZaKa img")?.Attributes["src"].Value);
                    var adUrl = HtmlEntity.DeEntitize(article.QuerySelector("a.Link_link__J4Qd8")?.Attributes["href"].Value);
                    var price = HtmlEntity.DeEntitize(article.QuerySelector(".AdItem_price__jUgxi")?.InnerText);

                    if (!price.Contains('€'))
                        continue;

                    var ad = new AdvertismentDto
                    {
                        Title = name,
                        Price = price,
                        Url = adUrl,
                        Image = imageSrc
                    };

                    advertisments.Add(ad);
                }
            }

            return advertisments;
        }
    }
}
