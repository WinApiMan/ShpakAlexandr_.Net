using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class DriverDto : IEntity
    {
        public DriverDto()
        {
        }

        public DriverDto(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private readonly ILazyLoader _lazyLoader;

        private ICollection<OrderDto> orders;

        private CarDto car;

        public int Id { get; set; }

        public int? CarId { get; set; }

        public int CallSign { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string DriverLicenseNumber { get; set; }

        public DateTime DateOfIssueOfDriversLicense { get; set; }

        public bool IsSickLeave { get; set; }

        public bool IsOnHoliday { get; set; }

        public ICollection<OrderDto> Orders
        {
            get => _lazyLoader.Load(this, ref orders);
            set => orders = value;
        }

        public CarDto Car
        {
            get => _lazyLoader.Load(this, ref car);
            set => car = value;
        }
    }
}