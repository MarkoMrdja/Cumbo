using Cumbo.Server.Data;
using Cumbo.Server.Models;
using Cumbo.Server.Repository.Interfaces;

namespace Cumbo.Server.Repository.Implementations
{
    public class ProductModelRepository : GenericRepository<ProductModel>, IProductModelRepository
    {
        public ProductModelRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
