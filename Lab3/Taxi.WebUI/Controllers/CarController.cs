using AutoMapper;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Interfaces;
using Taxi.WebUI.ViewModels;

namespace Taxi.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        private readonly ILogger<CarController> _logger;

        private readonly IMapper _mapper;

        public CarController(ICarService carService, ILogger<CarController> logger, IMapper mapper)
        {
            _carService = carService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ActionResult> Cars()
        {
            var carList = _mapper.Map<IEnumerable<CarViewModel>>(await _carService.GetAll());
            return View(carList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarViewModel carViewModel)
        {
            try
            {
                var car = _mapper.Map<Car>(carViewModel);
                if (await _carService.UniquenessCheck(car))
                {
                    await _carService.Add(car);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Car create error:{exception.Message}");
                return View(carViewModel);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var car = await _carService.FindById(id);
            var carViewModel = _mapper.Map<CarViewModel>(car);
            return View(carViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CarViewModel carViewModel)
        {
            try
            {
                var car = _mapper.Map<Car>(carViewModel);
                await _carService.Update(car);
                return RedirectToAction(nameof(Cars));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Car update error:{exception.Message}");
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var car = await _carService.FindById(id);
            var carViewModel = _mapper.Map<CarViewModel>(car);
            return View(carViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CarViewModel carViewModel)
        {
            try
            {
                await _carService.Delete(carViewModel.Id);
                return RedirectToAction(nameof(Cars));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Car delete error:{exception.Message}");
                return View();
            }
        }

        public async Task<ActionResult> CarsOnRepair()
        {
            var carsList = _mapper.Map<IEnumerable<CarViewModel>>(await _carService.GetCarsOnRepair());
            return View(carsList);
        }

        public async Task<ActionResult> NewCars(int age)
        {
            var carsList = _mapper.Map<IEnumerable<CarViewModel>>(await _carService.GetNewCars(age));
            return View(carsList);
        }
    }
}