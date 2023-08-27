using Cumbo.Server.Data;
using Cumbo.Server.Models;
using Cumbo.Server.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cumbo.Server.Repository.Implementations
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(AppDbContext context) : base(context)
        {
            
        }

        public override async Task<Manufacturer?> GetById(int id)
        {
            try
            {
                return await _context.Manufacturers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);       
            }
            catch
            {
                throw;
            }
        }
    }
}
