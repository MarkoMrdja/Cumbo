using Cumbo.Server.Data;
using Cumbo.Server.Models;
using Cumbo.Server.Repository.Interfaces;

namespace Cumbo.Server.Repository.Implementations
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
