using Cumbo.Server.Data;
using Cumbo.Server.Models;
using Cumbo.Shared;
using Microsoft.EntityFrameworkCore;

namespace Cumbo.Server.Services.ManufacturerService
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly AppDbContext _context;

        public ManufacturerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Manufacturer>>> GetAllManufacturers()
        {
            var response = new ServiceResponse<List<Manufacturer>>()
            {
                Data = await _context.Manufacturers.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Manufacturer>> GetManufacturer(int id)
        {
            var response = new ServiceResponse<Manufacturer>();
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);

            if (manufacturer is not null)
            {
                response.Data = manufacturer;
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "There isn't a manufacturer with given ID";
            }

            return response;
        }

        public async Task<ServiceResponse<Manufacturer>> UpdateManufacturer(Manufacturer manufacturer)
        {
            var response = new ServiceResponse<Manufacturer>();
            var existing_manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == manufacturer.Id);

            if (existing_manufacturer is null)
            {
                response.Success = false;
                response.Message = "Manufacturer could not be found.";

                return response;
            }

            existing_manufacturer.Name = manufacturer.Name;

            await _context.SaveChangesAsync();
            
            response.Success = true;
            response.Data = manufacturer;

            return response;
                
        }

        public async Task<ServiceResponse<Manufacturer>> AddManufacturer(Manufacturer manufacturer)
        {
            var response = new ServiceResponse<Manufacturer>();

            try
            {
                await _context.Manufacturers.AddAsync(manufacturer);

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = manufacturer;

                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                
                return response;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteManufacturer(int id)
        {
            var response = new ServiceResponse<bool>();

            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);

            if (manufacturer is null)
            {
                response.Success = false;
                response.Message = "Manufacturer with given ID doesn't exist";

                return response;
            }

            _context.Manufacturers.Remove(manufacturer);

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Manufacturer deleted successfully";

            return response;
        }
    }
}
