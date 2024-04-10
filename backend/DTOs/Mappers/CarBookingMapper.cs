using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    
    public static class CarBookingMapper
    {
        public static CarBookingDTOModel ToDTO(this CarBookingEFModel car)
        {
            //if (car == null) return null;
            return new CarBookingDTOModel
            {
                Id = car.Id,
                UserId = car.UserId,
                CarId = car.CarId,
                TotalPrice = car.TotalPrice,
                Duration = car.Duration,
                StartDate = car.StartDate,
                EndDate = car.EndDate,
                Status = (DTOModels.BookingStatus)car.Status
            };
        }

        public static CarBookingEFModel ToEFModel(this CarBookingDTOModel car)
        {
            //if (car == null) return null;
            return new CarBookingEFModel
            {
                Id = car.Id,
                UserId = car.UserId,
                CarId = car.CarId,
                TotalPrice = car.TotalPrice,
                Duration = car.Duration,
                StartDate = car.StartDate,
                EndDate = car.EndDate,
                Status = (DataAccessLayer.EFModels.BookingStatus)car.Status
            };
        }
    }
}
