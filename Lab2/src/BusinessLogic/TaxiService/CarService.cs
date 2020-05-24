using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Processings
{
    public class CarService
    {
        private readonly IRepository<CarDto> _carRepository;

        private readonly IRepository<DriverDto> _driverRepository;

        private readonly IMapper _mapper;

        public CarService(IRepository<CarDto> carRepository, IRepository<DriverDto> driverRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task AddCar(Car car)
        {
            var newCar = _mapper.Map<CarDto>(car);
            await _carRepository.Create(newCar);
        }

        public async Task DeleteCar(int id)
        {
            var car = await _carRepository.FindById(id);
            var driver = await _driverRepository.FindById(id);
            if (car != null && driver != null)
            {
                driver.CarId = null;
                await _driverRepository.Update(driver);
                await _carRepository.Remove(car);
            }
            else if (car != null && driver == null)
            {
                await _carRepository.Remove(car);
            }
        }

        public async Task<Car> FindById(int id)
        {
            return _mapper.Map<Car>(await _carRepository.FindById(id));
        }

        public async Task UpdateCar(Car car)
        {
            var newCar = _mapper.Map<CarDto>(car);
            await _carRepository.Update(newCar);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return _mapper.Map<IEnumerable<Car>>(await _carRepository.Get());
        }

        public async Task<IEnumerable<Car>> CarOnRepair()
        {
            var cars = await _carRepository.Get();
            return _mapper.Map<IEnumerable<Car>>(cars.Where(e => e.IsRepair));
        }

        public async Task<IEnumerable<Car>> GetOldCars(int age)
        {
            var cars = await _carRepository.Get();
            return _mapper.Map<IEnumerable<Car>>(cars.Where(e => DateTime.Now.Year - e.YearOfIssue <= age));
        }

        public async Task<Car> FindByGovernmentNumber(string governmentNumber)
        {
            var cars = await _carRepository.Get();
            cars.Where(e => e.GovernmentNumber.Equals(governmentNumber));
            return _mapper.Map<Car>(cars.First());
        }

        public async Task<bool> UniquenessCheck(Car car)
        {
            var cars = await _carRepository.Get();
            var resultOfFind = cars.FirstOrDefault(e => e.GovernmentNumber.Equals(car.GovernmentNumber) || e.RegistrationNumber.Equals(car.RegistrationNumber));
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