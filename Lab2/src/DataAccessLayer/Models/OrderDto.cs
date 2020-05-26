using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class OrderDto : IEntity
    {
        public OrderDto()
        {
        }

        public OrderDto(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        private readonly ILazyLoader _lazyLoader;

        private DriverDto driver;

        private ClientDto client;

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsDone { get; set; }

        public double Cost { get; set; }

        public double Distance { get; set; }

        public double Discount { get; set; }

        public int? DriverId { get; set; }

        public int ClientId { get; set; }

        public DriverDto Driver
        {
            get => _lazyLoader.Load(this, ref driver);
            set => driver = value;
        }

        public ClientDto Client
        {
            get => _lazyLoader.Load(this, ref client);
            set => client = value;
        }
    }
}