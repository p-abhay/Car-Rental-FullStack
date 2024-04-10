using BusinessLayer.IServices;
using DataAccessLayer.IRepository;
using DTOs.DTOModels;
using DTOs.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CarDTOModel> AddCar(CarDTOModel car)
        {
            var carEF = CarMapper.ToEFModel(car);
            var addedCar = await _carRepository.AddCar(carEF);
            return CarMapper.ToDTO(addedCar);
        }

        public async Task<CarDTOModel> DeleteCar(Guid carId)
        {
            var deletedCar = await _carRepository.DeleteCar(carId);

            return CarMapper.ToDTO(deletedCar);
        }

        public async Task<IEnumerable<CarDTOModel>> GetAllCars()
        {
            var carEFList = await _carRepository.GetAllCars();

            var cars = carEFList.Select(c => CarMapper.ToDTO(c));
            return cars;
        }

        public async Task<IEnumerable<CarDTOModel>> GetAvailableCars()
        {
            var carEFList = await _carRepository.GetAvailableCars();

            var cars = carEFList.Select(c => CarMapper.ToDTO(c));
            return cars;
        }

        public async Task<CarDTOModel> GetCarById(Guid carId)
        {
            var carEF = await _carRepository.GetCarById(carId);
            
            return CarMapper.ToDTO(carEF);
        }

        public async Task<CarDTOModel> UpdateCar(CarDTOModel updatedCar)
        {
            var carEF = CarMapper.ToEFModel(updatedCar);
            var updatedEFCar = await _carRepository.UpdateCar(carEF);

            return CarMapper.ToDTO(updatedEFCar);
        }
    }
}
