﻿using Cumbo.Server.Data;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Server.Repository.Interfaces;
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
    }
}
