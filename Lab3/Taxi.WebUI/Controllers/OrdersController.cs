﻿using AutoMapper;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Interfaces;
using Taxi.WebUI.ViewModels;

namespace Taxi.WebUI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IDriverService _driverService;

        private readonly IOrderService _orderService;

        private readonly ILogger<CarsController> _logger;

        private readonly IMapper _mapper;

        public OrdersController(IDriverService driverService, IOrderService orderService, ILogger<CarsController> logger, IMapper mapper)
        {
            _driverService = driverService;
            _orderService = orderService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActionResult> Orders()
        {
            var ordersList = _mapper.Map<IEnumerable<OrderViewModel>>(await _orderService.GetAll());

            foreach (var item in ordersList)
            {
                try
                {
                    item.Driver = _mapper.Map<DriverViewModel>(await _driverService.FindById((int)item.DriverId));
                }
                catch (InvalidOperationException exception)
                {
                    _logger.LogError($"Find order error:{exception.Message}");
                }
            }
            return View(ordersList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Orders));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var order = await _orderService.FindById(id);
                var orderViewModel = _mapper.Map<OrderViewModel>(order);
                return View(orderViewModel);
            }
            catch (ArgumentException exception)
            {
                _logger.LogError(exception.Message);
                return RedirectToAction(nameof(Orders));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderViewModel orderViewModel)
        {
            try
            {
                var order = _mapper.Map<Order>(orderViewModel);
                await _orderService.Update(order);
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Order update error:{exception.Message}");
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var order = await _orderService.FindById(id);
                var orderViewModel = _mapper.Map<OrderViewModel>(order);
                return View(orderViewModel);
            }
            catch (ArgumentException exception)
            {
                _logger.LogError(exception.Message);
                return RedirectToAction(nameof(Orders));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(OrderViewModel orderViewModel)
        {
            try
            {
                await _orderService.Delete(orderViewModel.Id);
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Car driver error:{exception.Message}");
                return View();
            }
        }

        public async Task<ActionResult> ActiveOrders()
        {
            var ordersList = _mapper.Map<IEnumerable<OrderViewModel>>(await _orderService.GetActiveOrders());
            return View(ordersList);
        }

        public async Task<ActionResult> InActiveOrders()
        {
            var ordersList = _mapper.Map<IEnumerable<OrderViewModel>>(await _orderService.GetInActiveOrders());
            return View(ordersList);
        }
    }
}