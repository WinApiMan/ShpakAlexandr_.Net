using Microsoft.EntityFrameworkCore.Infrastructure;
using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class CarDto : IEntity
    {
        private readonly ILazyLoader _lazyLoader;

        public CarDto()
        {
        }

        public CarDto(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }

        public string GovernmentNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int YearOfIssue { get; set; }

        public string RegistrationNumber { get; set; }

        public bool IsRepair { get; set; }

        private DriverDto driver;

        public DriverDto Driver
        {
            get => _lazyLoader.Load(this, ref driver);
            set => driver = value;
        }
    }
}