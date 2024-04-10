using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface ICarBookingService
    {
        public Task<IEnumerable<CarBookingDTOModel>> GetAllCarBookings();
        public Task<CarBookingDTOModel> GetCarBookingById(Guid id);
        public Task<CarBookingDTOModel> AddCarBooking(CarBookingDTOModel car);
        public Task<CarBookingDTOModel> DeleteCarBooking(Guid id);
        public Task<CarBookingDTOModel> UpdateCarBooking(CarBookingDTOModel updatedCar);
        public Task<IEnumerable<CarBookingDTOModel>> GetCarBookingByUserId(Guid userId);
    }
}
