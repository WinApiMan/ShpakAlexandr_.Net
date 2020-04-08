using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using System.Collections.Generic;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace BusinessLogic.Services.ReadWriteServices
{
    public class DataBaseFromFile
    {
        private readonly IRepository<Client> _clientsRepository;

        private readonly IRepository<Car> _carsRepository;

        private readonly IRepository<Driver> _driversRepository;

        private readonly IRepository<Order> _ordersRepository;

        private readonly IMapper _mapper;

        public DataBaseFromFile(IRepository<Client> clientsRepository, IRepository<Car> carsRepository, IRepository<Driver> driversRepository, IRepository<Order> ordersRepository, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
            _carsRepository = carsRepository;
            _driversRepository = driversRepository;
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public void ImportAndExportData(IWriter writer, string carsPath, string clientsPath, string driversPath, string ordersPath)
        {
            //Read data
            var cars = _mapper.Map<IEnumerable<CarDto>>(_carsRepository.Get());
            var clients = _mapper.Map<IEnumerable<ClientDto>>(_clientsRepository.Get());
            var drivers = _mapper.Map<IEnumerable<DriverDto>>(_driversRepository.Get());
            var orders = _mapper.Map<IEnumerable<OrderDto>>(_ordersRepository.Get());

            //Write data
            writer.Write(cars, carsPath);
            writer.Write(clients, clientsPath);
            writer.Write(drivers, driversPath);
            writer.Write(orders, ordersPath);
        }
    }
}