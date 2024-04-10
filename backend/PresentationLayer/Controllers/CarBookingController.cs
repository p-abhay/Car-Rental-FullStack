using BusinessLayer.IServices;
using BusinessLayer.Services;
using DTOs.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/car-bookings")]
    [ApiController]
    public class CarBookingController : ControllerBase
    {
        private readonly ICarBookingService _carBookingService;

        public CarBookingController(ICarBookingService carBookingService)
        {
            _carBookingService = carBookingService;
        }

        [Route("/api/car-bookings/all")]
        [HttpGet]
        public async Task<IActionResult> GetAllCarBookings()
        {
            try
            {
                var cars = await _carBookingService.GetAllCarBookings();
                if (cars == null)
                    return NotFound();
                return Ok(cars);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Route("/api/car/id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarBookingById(Guid id)
        {
            try
            {
                var car = await _carBookingService.GetCarBookingById(id);
                if (car == null)
                    return NotFound();
                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCarBookingByUserId(Guid userId)
        {
            try
            {
                var bookings = await _carBookingService.GetCarBookingByUserId(userId);
                if (bookings == null)
                    return NotFound();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/api/car-booking/add")]
        [HttpPost]
        public async Task<IActionResult> AddCarBooking([FromBody] CarBookingDTOModel car)
        {
            try
            {
                car.Id = Guid.NewGuid();
                var addedCar = await _carBookingService.AddCarBooking(car);
                return Ok(addedCar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/api/car-booking/update")]
        [HttpPut]
        public async Task<IActionResult> UpdateCarBooking([FromBody] CarBookingDTOModel car)
        {
            try
            {
                var updatedCar = await _carBookingService.UpdateCarBooking(car);
                return Ok(updatedCar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        //[Route("/api/car/delete")]

        public async Task<IActionResult> DeleteCarBooking(Guid id)
        {
            try
            {
                var delete = await _carBookingService.DeleteCarBooking(id);
                return Ok(delete);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
