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
    public class CarBookingRepository : ICarBookingRepository
    {
        private readonly CarRentalDbContext _carRentalDbContext;

        public CarBookingRepository(CarRentalDbContext carRentalDbContext) 
        {
            _carRentalDbContext = carRentalDbContext;
        }

        public async Task<CarBookingEFModel> AddCarBooking(CarBookingEFModel bookedCar)
        {
            await _carRentalDbContext.CarBookings.AddAsync(bookedCar);
            await _carRentalDbContext.SaveChangesAsync();
            //update availability status 
            var car = await _carRentalDbContext.Cars.Where(c => c.Id == bookedCar.CarId).FirstOrDefaultAsync();

            car.AvailabilityStatus = false;

             _carRentalDbContext.Cars.Update(car);
            await _carRentalDbContext.SaveChangesAsync();
            return bookedCar;
        }

        public async Task<CarBookingEFModel> DeleteCarBooking(Guid id)
        {
            var delete = await GetCarBookingById(id);
            //get carid for the current booking;
            var car = await GetCarBookingById(id);
            var carId = car.CarId;
            //update availability status 
            var updateCar = await _carRentalDbContext.Cars.Where(c => c.Id == carId).FirstOrDefaultAsync();
            updateCar.AvailabilityStatus = true;
            _carRentalDbContext.Cars.Update(updateCar);
            await _carRentalDbContext.SaveChangesAsync();
            //Now delete the booking history
            _carRentalDbContext.CarBookings.Remove(delete);
            await _carRentalDbContext.SaveChangesAsync();

            return delete;
        }

        public async Task<IEnumerable<CarBookingEFModel>> GetAllCarBookings()
        {
            return await _carRentalDbContext.CarBookings.ToListAsync();
        }

        public async Task<CarBookingEFModel> GetCarBookingById(Guid id)
        {
            var car = await _carRentalDbContext.CarBookings.Where(c => c.Id == id).FirstOrDefaultAsync();

            return car;
        }

        public async Task<IEnumerable<CarBookingEFModel>> GetCarBookingByUserId(Guid userId)
        {
            var userBooking = await _carRentalDbContext.CarBookings.Where(b => b.UserId == userId).ToListAsync();

            return userBooking;

        }

        public async Task<CarBookingEFModel> UpdateCarBooking(CarBookingEFModel updatedCar)
        {
            _carRentalDbContext.CarBookings.Update(updatedCar);
            await _carRentalDbContext.SaveChangesAsync();

            return updatedCar;
        }
    }
}
