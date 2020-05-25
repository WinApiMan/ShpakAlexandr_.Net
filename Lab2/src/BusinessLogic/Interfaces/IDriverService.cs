using BusinessLogic.Models;
using System.Threading.Tasks;

namespace Taxi.BusinessLogic.Interfaces
{
    public interface IDriverService : IManager<Driver>
    {
        Task<Driver> FindByDriverLicenseNumber(string licenseNumber);

        Task GiveCar(int driverId, int carId);

        Task<bool> UniquenessCheck(Driver driver);
    }
}