using BusinessLayer.IServices;
using DataAccessLayer.EFModels;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using DTOs.DTOModels;
using DTOs.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CarBookingService : ICarBookingService
    {
        private readonly ICarBookingRepository _carBookingRepository;
        

        public CarBookingService(ICarBookingRepository carBookingRepository) 
        { 
            _carBookingRepository = carBookingRepository;
            
        }
        public async Task<CarBookingDTOModel> AddCarBooking(CarBookingDTOModel bookedCar)
        {
            var bookedCarEF = CarBookingMapper.ToEFModel(bookedCar);
            bookedCarEF.StartDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            bookedCarEF.EndDate = DateTime.Now.AddDays(bookedCarEF.Duration).ToString("dd-MM-yyyy HH:mm:ss");
            var addedCarBooking = await _carBookingRepository.AddCarBooking(bookedCarEF);
            return CarBookingMapper.ToDTO(addedCarBooking);
            /*if(addedCarBooking != null)
            {
                var car = await _carService.GetCarById(bookedCar.CarId);
                var updatedCar = new CarDTOModel
                {
                    Id = car.Id,
                    Maker = car.Maker,
                    Model = car.Model,
                    AvailabilityStatus = false,
                    Image = car.Image,
                    RentalPrice = car.RentalPrice
                };
                var updatedCarDTO = await _carService.UpdateCar(car);
                if(updatedCarDTO != null)
                {
                    return CarBookingMapper.ToDTO(addedCarBooking);
                }
            }*/
            //return null;
        }

        public async Task<CarBookingDTOModel> DeleteCarBooking(Guid id)
        {
            var deletedCar = await _carBookingRepository.DeleteCarBooking(id);

            return CarBookingMapper.ToDTO(deletedCar);
        }

        public async Task<IEnumerable<CarBookingDTOModel>> GetAllCarBookings()
        {
            var carEFList = await _carBookingRepository.GetAllCarBookings();

            var cars = carEFList.Select(c => CarBookingMapper.ToDTO(c));
            return cars;
        }

        public async Task<CarBookingDTOModel> GetCarBookingById(Guid id)
        {
            var carEF = await _carBookingRepository.GetCarBookingById(id);
            return CarBookingMapper.ToDTO(carEF);
        }

        public async Task<IEnumerable<CarBookingDTOModel>> GetCarBookingByUserId(Guid userId)
        {
            var userBookingsEF = await _carBookingRepository.GetCarBookingByUserId(userId);
            var userBookings = userBookingsEF.Select(b => CarBookingMapper.ToDTO(b));

            return userBookings;
        }

        public async Task<CarBookingDTOModel> UpdateCarBooking(CarBookingDTOModel updatedCar)
        {
            var carEF = CarBookingMapper.ToEFModel(updatedCar);
            carEF.StartDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            carEF.EndDate = DateTime.Now.AddDays(carEF.Duration).ToString("dd-MM-yyyy HH:mm:ss");
            var updatedEFCar = await _carBookingRepository.UpdateCarBooking(carEF);

            return CarBookingMapper.ToDTO(updatedEFCar);
        }
    }
}
