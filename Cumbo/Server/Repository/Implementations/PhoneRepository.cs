using Cumbo.Server.Data;
using Cumbo.Server.Models;
using Cumbo.Server.Repository.Interfaces;

namespace Cumbo.Server.Repository.Implementations
{
    public class PhoneRepository : GenericRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
