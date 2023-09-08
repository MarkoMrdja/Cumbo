using Cumbo.Client.Features;
using Cumbo.Shared.DTOs.Advertisement;
using Cumbo.Shared.Entities;
using Cumbo.Shared.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Cumbo.Client.Services.AdvertisementService
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdvertisementService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagingResponse<Advertisment>> GetAds(AdvertisementParameters advertisementParameters)
        {
            var client = _httpClientFactory.CreateClient("Base");

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = advertisementParameters.PageNumber.ToString()
            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("/api/Advertisment/GetActive", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<Advertisment>
            {
                Items = JsonConvert.DeserializeObject<List<Advertisment>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }
    }
}
