using Cumbo.Server.Data;
using Cumbo.Server.Repository.Interfaces;
using System.Transactions;

namespace Cumbo.Server.Repository.Implementations
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
