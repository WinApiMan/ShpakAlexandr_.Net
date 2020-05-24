using System;
using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class OrderDto : IEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsDone { get; set; }

        public double Cost { get; set; }

        public double Distance { get; set; }

        public double Discount { get; set; }

        public int? DriverId { get; set; }

        public int ClientId { get; set; }

        public virtual DriverDto Driver { get; set; }

        public virtual ClientDto Client { get; set; }
    }
}