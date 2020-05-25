using BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taxi.BusinessLogic.Interfaces
{
    public interface ICarService : IManager<Car>
    {
        Task<IEnumerable<Car>> CarOnRepair();

        Task<IEnumerable<Car>> GetOldCars(int age);

        Task<Car> FindByGovernmentNumber(string governmentNumber);

        Task<bool> UniquenessCheck(Car car);
    }
}