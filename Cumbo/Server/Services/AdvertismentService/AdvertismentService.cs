using AutoMapper;
using Cumbo.Server.Data;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cumbo.Server.Services.AdvertismentService
{
    public class AdvertismentService : IAdvertismentService
    {
        private readonly AppDbContext _context;

        public AdvertismentService(AppDbContext context)
        {
            _context = context;
        }


    }
}
