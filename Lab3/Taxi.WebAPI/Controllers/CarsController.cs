using AutoMapper;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Interfaces;

namespace Taxi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> Get()
        {
            return (await _carService.GetAll()).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            try
            {
                var car = await _carService.FindById(id);
                return Ok(car);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Car>> Post(Car car)
        {
            try
            {
                await _carService.Add(car);
                return Ok(car);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> Put(Car car)
        {
            try
            {
                await _carService.Update(car);
                return Ok(car);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(int id)
        {
            try
            {
                await _carService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
