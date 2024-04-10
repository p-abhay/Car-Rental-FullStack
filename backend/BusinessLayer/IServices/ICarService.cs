using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface ICarService
    {
        public Task<IEnumerable<CarDTOModel>> GetAllCars();
        public Task<IEnumerable<CarDTOModel>> GetAvailableCars();
        public Task<CarDTOModel> GetCarById(Guid carId);
        public Task<CarDTOModel> AddCar(CarDTOModel car);
        public Task<CarDTOModel> UpdateCar(CarDTOModel updatedCar);
        public Task<CarDTOModel> DeleteCar(Guid carId);
    }
}
