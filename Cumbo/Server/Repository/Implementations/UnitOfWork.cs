using Cumbo.Server.Data;
using Cumbo.Server.Repository.Interfaces;

namespace Cumbo.Server.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Manufacturer = new ManufacturerRepository(_context);
            ProductModel = new ProductModelRepository(_context);
            Phone = new PhoneRepository(_context);
            User = new UserRepository(_context);
            Customer = new CustomerRepository(_context);
            Transaction = new TransactionRepository(_context);
            Advertisement = new AdvertisementRepository(_context);
        }

        public IManufacturerRepository Manufacturer { get; private set; }

        public IProductModelRepository ProductModel { get; private set; }

        public IPhoneRepository Phone { get; private set; }

        public IUserRepository User { get; private set; }

        public ICustomerRepository Customer { get; private set; }

        public ITransactionRepository Transaction { get; private set; }

        public IAdvertisementRepository Advertisement { get; private set; }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
