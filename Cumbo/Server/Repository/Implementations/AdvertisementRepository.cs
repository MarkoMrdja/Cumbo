using Cumbo.Server.Data;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Paging;
using Cumbo.Server.Repository.Interfaces;
using Cumbo.Shared.DTOs.Advertisement;
using Cumbo.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Cumbo.Server.Repository.Implementations
{
    public class AdvertisementRepository : GenericRepository<Advertisment>, IAdvertisementRepository
    {
        public AdvertisementRepository(AppDbContext context) : base(context)
        {
            
        }


        public async Task<List<Advertisment>> GetActive()
        {
            try
            {
                return await _context.Advertisments.Where(x => x.CurrentlyActive).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<PagedList<Advertisment>> GetActivePaged(AdvertisementParameters advertisementParameters)
        {
            try
            {
                var activeAds = await _context.Advertisments.Where(x => x.CurrentlyActive).ToListAsync();

                return PagedList<Advertisment>
                    .ToPagedList(activeAds, advertisementParameters.PageNumber, advertisementParameters.PageSize);
            }
            catch
            {
                throw;
            }
        }
    }
}
