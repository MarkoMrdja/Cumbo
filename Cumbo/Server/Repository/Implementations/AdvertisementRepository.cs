using Cumbo.Server.Data;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Repository.Interfaces;

namespace Cumbo.Server.Repository.Implementations
{
    public class AdvertisementRepository : GenericRepository<Advertisment>, IAdvertisementRepository
    {
        public AdvertisementRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
