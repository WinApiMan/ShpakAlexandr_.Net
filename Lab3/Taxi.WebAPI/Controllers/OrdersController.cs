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
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }


        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return (await _orderService.GetAll()).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            try
            {
                var order = await _orderService.FindById(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            try
            {
                await _orderService.Add(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            try
            {
                await _orderService.Update(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return BadRequest();
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            try
            {
                await _orderService.Delete(id);
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
