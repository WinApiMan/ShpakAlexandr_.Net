﻿using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Interfaces;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Services
{
    public class DriverService : IDriverService
    {
        private readonly IRepository<DriverDto> _driverRepository;

        private readonly IMapper _mapper;

        public DriverService(IRepository<DriverDto> driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task Add(Driver driver)
        {
            var newDriver = _mapper.Map<DriverDto>(driver);
            await _driverRepository.Create(newDriver);
        }

        public async Task Delete(int id)
        {
            var driver = await _driverRepository.FindById(id);
            await _driverRepository.Remove(driver);
        }

        public async Task Update(Driver driver)
        {
            var newDriver = _mapper.Map<DriverDto>(driver);
            await _driverRepository.Update(newDriver);
        }

        public async Task<Driver> FindById(int id)
        {
            if (id > 0)
            {
                return _mapper.Map<Driver>(await _driverRepository.FindById(id));
            }
            else
            {
                throw new ArgumentException("Id less then zero");
            }
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            return _mapper.Map<IEnumerable<Driver>>(await _driverRepository.Get());
        }

        public async Task<Driver> FindByDriverLicenseNumber(string licenseNumber)
        {
            var drivers = await _driverRepository.Get();
            return _mapper.Map<Driver>(drivers.FirstOrDefault(e => e.DriverLicenseNumber.Equals(licenseNumber)));
        }

        public async Task GiveCar(int driverId, int carId)
        {
            var driver = await _driverRepository.FindById(driverId);
            driver.CarId = carId;
            await _driverRepository.Update(driver);
        }

        public async Task<bool> UniquenessCheck(Driver driver)
        {
            var drivers = await _driverRepository.Get();
            try
            {
                var resultOfFind = drivers.Single(e => e.DriverLicenseNumber.Equals(driver.DriverLicenseNumber) || e.DateOfIssueOfDriversLicense.Equals(driver.DateOfIssueOfDriversLicense) || e.CallSign == driver.CallSign);
                return false;
            }
            catch (ArgumentNullException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }
    }
}