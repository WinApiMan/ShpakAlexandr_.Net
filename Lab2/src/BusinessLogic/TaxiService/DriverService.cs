using AutoMapper;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Processings
{
    public class DriverService
    {
        private readonly IRepository<Driver> _driverRepository;

        private readonly IMapper _mapper;

        public DriverService(IRepository<Driver> driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public void AddDriver(DriverDto driver)
        {
            var newDriver = _mapper.Map<Driver>(driver);
            _driverRepository.Create(newDriver);
        }

        public bool DeleteDriver(int id)
        {
            try
            {
                var driver = _driverRepository.FindById(id);
                _driverRepository.Remove(driver);
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (DbException)
            {
                return false;
            }
        }

        public void UpdateDriver(DriverDto driver)
        {
            var newDriver = _mapper.Map<Driver>(driver);
            _driverRepository.Update(newDriver);
        }

        public DriverDto FindById(int id)
        {
            return _mapper.Map<DriverDto>(_driverRepository.FindById(id));
        }

        public IEnumerable<DriverDto> GetAll()
        {
            return _mapper.Map<IEnumerable<DriverDto>>(_driverRepository.Get());
        }

        public DriverDto FindByDriverLicenseNumber(string licenseNumber)
        {
            return _mapper.Map<DriverDto>(_driverRepository.Get(e => e.DriverLicenseNumber.Equals(licenseNumber)).First());
        }

        public bool GiveCar(int driverId, int carId)
        {
            try
            {
                var driver = _driverRepository.FindById(driverId);
                driver.CarId = carId;
                _driverRepository.Update(driver);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UniquenessCheck(DriverDto driver)
        {
            var resultOfFind = _driverRepository.Get().FirstOrDefault(e => e.DriverLicenseNumber.Equals(driver.DriverLicenseNumber) || e.DateOfIssueOfDriversLicense.Equals(driver.DateOfIssueOfDriversLicense) || e.CallSign == driver.CallSign);
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