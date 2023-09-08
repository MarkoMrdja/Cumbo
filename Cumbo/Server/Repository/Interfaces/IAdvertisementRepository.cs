using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Paging;
using Cumbo.Shared.DTOs.Advertisement;
using Cumbo.Shared.RequestFeatures;

namespace Cumbo.Server.Repository.Interfaces
{
    public interface IAdvertisementRepository : IGenericRepository<Advertisment>
    {
        Task<List<Advertisment>> GetActive();
        Task<PagedList<Advertisment>> GetActivePaged(AdvertisementParameters advertisementParameters);
    }
}
