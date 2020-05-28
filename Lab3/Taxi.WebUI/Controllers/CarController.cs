using AutoMapper;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        // GET: Car
        public async Task<ActionResult> Index()
        {
            var carList = _mapper.Map<IEnumerable<CarViewModel>>(await _carService.GetAll());
            return View(carList);
        }

        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarViewModel car)
        {
            try
            {
                var newCar = _mapper.Map<Car>(car);
                _carService.Add(newCar);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(car);
            }
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Car/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}