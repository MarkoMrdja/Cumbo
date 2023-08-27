using Cumbo.Server.Repository.Interfaces;

namespace Cumbo.Server.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IManufacturerRepository Manufacturer { get; }
        IProductModelRepository ProductModel { get; }
        IPhoneRepository Phone { get; }
        IUserRepository User { get; }
        ICustomerRepository Customer { get; }
        ITransactionRepository Transaction { get; }
        IAdvertisementRepository Advertisement { get; }
        Task<int> Save();
    }
}
