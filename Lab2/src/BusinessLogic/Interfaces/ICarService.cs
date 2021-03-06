﻿using BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taxi.BusinessLogic.Interfaces
{
    public interface ICarService : IManager<Car>
    {
        Task<IEnumerable<Car>> GetCarsOnRepair();

        Task<IEnumerable<Car>> GetNewCars(int age);

        Task<Car> FindByGovernmentNumber(string governmentNumber);

        Task<bool> UniquenessCheck(Car car);
    }
}