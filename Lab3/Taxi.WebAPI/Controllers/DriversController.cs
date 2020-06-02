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
    public class DriversController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly ILogger<DriversController> _logger;

        public DriversController(IDriverService driverService, ILogger<DriversController> logger)
        {
            _driverService = driverService;
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> Get()
        {
            return (await _driverService.GetAll()).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> Get(int id)
        {
            try
            {
                var driver = await _driverService.FindById(id);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Driver>> Post(Driver driver)
        {
            try
            {
                await _driverService.Add(driver);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Driver>> Put(Driver driver)
        {
            try
            {
                await _driverService.Update(driver);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Driver>> Delete(int id)
        {
            try
            {
                await _driverService.Delete(id);
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
