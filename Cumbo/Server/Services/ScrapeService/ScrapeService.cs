using Cumbo.Server.Models.KupujemProdajemAd;
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

        public List<Advertisment> ScrapeAdsPerPage(int page)
        {
            try
            {
                var document = _Web.Load($"https://novi.kupujemprodajem.com/aleksandar-ns-0654389483/svi-oglasi/470651/{page}?order=price");

                var articleElements = document.DocumentNode.QuerySelectorAll("article.AdItem_adHolder__NoNLJ");

                List<Advertisment> advertisments = new();

                foreach (var article in articleElements)
                {
                    var name = HtmlEntity.DeEntitize(article.QuerySelector(".AdItem_name__RhGAZ")?.InnerText);
                    var imageSrc = HtmlEntity.DeEntitize(article.QuerySelector(".AdItem_imageHolder__LZaKa img")?.Attributes["src"].Value);
                    var adUrl = HtmlEntity.DeEntitize(article.QuerySelector("a.Link_link__J4Qd8")?.Attributes["href"].Value);
                    var price = HtmlEntity.DeEntitize(article.QuerySelector(".AdItem_price__jUgxi")?.InnerText);

                    var ad = new Advertisment
                    {
                        Title = name,
                        Price = price,
                        Url = adUrl,
                        Image = imageSrc
                    };

                    advertisments.Add(ad);
                }

                //if (advertisments.Count is 0) handle this situation


                return advertisments;
            }
            catch
            {
                return new List<Advertisment>(); //make this better
            }
            


        }

    }
}
