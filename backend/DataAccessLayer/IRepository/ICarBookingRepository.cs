using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ICarBookingRepository
    {
        public Task<IEnumerable<CarBookingEFModel>> GetAllCarBookings();
        public Task<CarBookingEFModel> GetCarBookingById(Guid id);
        public Task<CarBookingEFModel> AddCarBooking(CarBookingEFModel car);
        public Task<CarBookingEFModel> DeleteCarBooking(Guid id);
        public Task<CarBookingEFModel> UpdateCarBooking(CarBookingEFModel updatedCar);
        public Task<IEnumerable<CarBookingEFModel>> GetCarBookingByUserId(Guid userId);
    }
}
