using System;
using System.Collections.Generic;
using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class DriverDto : IEntity
    {
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

        public virtual ICollection<OrderDto> Orders { get; set; }

        public virtual CarDto Car { get; set; }
    }
}