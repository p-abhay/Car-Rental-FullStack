using DataAccessLayer.DBContext;
using DataAccessLayer.EFModels;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _carRentalDbContext;

        public CarRepository(CarRentalDbContext carRentalDbContext)
        {
            _carRentalDbContext = carRentalDbContext;
        }

        public async Task<CarEFModel> AddCar(CarEFModel car)
        {
            await _carRentalDbContext.Cars.AddAsync(car);
            await _carRentalDbContext.SaveChangesAsync();

            return car;
        }

        public async Task<CarEFModel> DeleteCar(Guid id)
        {
            var delete = await GetCarById(id);
            _carRentalDbContext.Cars.Remove(delete);
            await _carRentalDbContext.SaveChangesAsync();

            return delete;
        }

        public async Task<IEnumerable<CarEFModel>> GetAvailableCars()
        {
            
            return await _carRentalDbContext.Cars.Where(c => c.AvailabilityStatus == true).ToListAsync();
        }

        public async Task<IEnumerable<CarEFModel>> GetAllCars()
        {
            return await _carRentalDbContext.Cars.ToListAsync();
        }

        public async Task<CarEFModel> GetCarById(Guid id)
        {
            var car = await _carRentalDbContext.Cars.Where(c =>  c.Id == id).FirstOrDefaultAsync();

            return car;
        }

        public async Task<CarEFModel> UpdateCar(CarEFModel updatedCar)
        {
            _carRentalDbContext.Cars.Update(updatedCar);
            await _carRentalDbContext.SaveChangesAsync();

            return updatedCar;
        }
    }
}
