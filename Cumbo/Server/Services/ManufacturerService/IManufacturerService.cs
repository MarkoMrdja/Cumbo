using Cumbo.Server.Models;
using Cumbo.Shared;

namespace Cumbo.Server.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task<ServiceResponse<List<Manufacturer>>> GetAllManufacturers();
        Task<ServiceResponse<Manufacturer>> GetManufacturer(int id);
        Task<ServiceResponse<Manufacturer>> UpdateManufacturer(Manufacturer manufacturer);
        Task<ServiceResponse<Manufacturer>> AddManufacturer(Manufacturer manufacturer);
        Task<ServiceResponse<bool>> DeleteManufacturer(int id);
    }
}
