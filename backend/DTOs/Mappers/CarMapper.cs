using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public static class CarMapper
    {
        public static CarDTOModel ToDTO(this CarEFModel car)
        {
            //if (car == null) return null;
            return new CarDTOModel
            {
                Id = car.Id,
                Maker = car.Maker,
                Model = car.Model,
                AvailabilityStatus = car.AvailabilityStatus,
                RentalPrice = car.RentalPrice,
                Image = car.Image
            };
        }

        public static CarEFModel ToEFModel(this CarDTOModel car)
        {
            //if (car == null) return null;
            return new CarEFModel
            {
                Id = car.Id,
                Maker = car.Maker,
                Model = car.Model,
                AvailabilityStatus = car.AvailabilityStatus,
                RentalPrice = car.RentalPrice,
                Image = car.Image
            };
        }
    }
}
