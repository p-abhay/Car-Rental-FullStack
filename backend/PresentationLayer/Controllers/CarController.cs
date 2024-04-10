using BusinessLayer.IServices;
using DTOs.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [Route("/api/car/all")]
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var cars = await _carService.GetAllCars();
                if (cars == null)
                    return NotFound();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("/api/car/available")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableCars()
        {
            try
            {
                var cars = await _carService.GetAvailableCars();
                if (cars == null)
                    return NotFound();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[Route("/api/car/id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            try
            {
                var car = await _carService.GetCarById(id);
                if (car == null)
                    return NotFound();
                return Ok(car);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("/api/car/add")]
        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarDTOModel car)
        {
            try
            {
                car.Id = Guid.NewGuid();
                var addedCar = await _carService.AddCar(car);
                return Ok(addedCar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/api/car/update")]
        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromBody] CarDTOModel car)
        {
            try
            {
                var updatedCar = await _carService.UpdateCar(car);
                return Ok(updatedCar);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        //[Route("/api/car/delete")]

        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                var deletedCarBooking = await _carService.DeleteCar(id);
                return Ok(deletedCarBooking);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
