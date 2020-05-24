using Taxi.DAL.Interfaces;

namespace Taxi.DAL.Models
{
    public class CarDto : IEntity
    {
        public int Id { get; set; }

        public string GovernmentNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int YearOfIssue { get; set; }

        public string RegistrationNumber { get; set; }

        public bool IsRepair { get; set; }

        public virtual DriverDto Driver { get; set; }
    }
}