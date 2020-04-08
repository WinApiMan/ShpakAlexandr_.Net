using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Processings
{
    public class CarService
    {
        private readonly IRepository<Car> _carRepository;

        private readonly IRepository<Driver> _driverRepository;

        private readonly IMapper _mapper;

        public CarService(IRepository<Car> carRepository, IRepository<Driver> driverRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public void AddCar(CarDto car)
        {
            var newCar = _mapper.Map<Car>(car);
            _carRepository.Create(newCar);
        }

        public bool DeleteCar(int id)
        {
            var car = _carRepository.FindById(id);
            var driver = _driverRepository.FindById(id);
            if (car != null && driver != null)
            {
                driver.CarId = null;
                _driverRepository.Update(driver);
                _carRepository.Remove(car);
                return true;
            }
            else if (car != null && driver == null)
            {
                _carRepository.Remove(car);
                return true;
            }
            else
            {
                return false;
            }
        }

        public CarDto FindById(int id)
        {
            return _mapper.Map<CarDto>(_carRepository.FindById(id));
        }

        public void UpdateCar(CarDto car)
        {
            var newCar = _mapper.Map<Car>(car);
            _carRepository.Update(newCar);
        }

        public IEnumerable<CarDto> GetAll()
        {
            return _mapper.Map<IEnumerable<CarDto>>(_carRepository.Get());
        }

        public IEnumerable<CarDto> CarOnRepair()
        {
            return _mapper.Map<IEnumerable<CarDto>>(_carRepository.Get(e => e.IsRepair));
        }

        public IEnumerable<CarDto> GetOldCars(int age)
        {
            return _mapper.Map<IEnumerable<CarDto>>(_carRepository.Get(e => DateTime.Now.Year - e.YearOfIssue <= age));
        }

        public CarDto FindByGovernmentNumber(string governmentNumber)
        {
            try
            {
                return _mapper.Map<CarDto>(_carRepository.Get(e => e.GovernmentNumber.Equals(governmentNumber)).First());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UniquenessCheck(CarDto car)
        {
            var resultOfFind = _carRepository.Get().FirstOrDefault(e => e.GovernmentNumber.Equals(car.GovernmentNumber) || e.RegistrationNumber.Equals(car.RegistrationNumber));
            if (resultOfFind != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}