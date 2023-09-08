using Cumbo.Client.Features;
using Cumbo.Shared.DTOs.Advertisement;
using Cumbo.Shared.RequestFeatures;

namespace Cumbo.Client.Services.AdvertisementService
{
    public interface IAdvertisementService
    {
        Task<PagingResponse<Advertisment>> GetAds(AdvertisementParameters advertisementParameters);
    }
}

