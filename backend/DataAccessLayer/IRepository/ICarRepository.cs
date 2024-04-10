using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ICarRepository
    {
        public Task<IEnumerable<CarEFModel>> GetAllCars();
        public Task<IEnumerable<CarEFModel>> GetAvailableCars();
        public Task<CarEFModel> GetCarById(Guid id);
        public Task<CarEFModel> AddCar(CarEFModel car);
        public Task<CarEFModel> DeleteCar(Guid id);
        public Task<CarEFModel> UpdateCar(CarEFModel updatedCar);


    }
}
