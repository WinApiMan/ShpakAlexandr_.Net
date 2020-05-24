using AutoMapper;
using BusinessLogic.Interfaces;
using System.Collections.Generic;
using Taxi.DAL.Interfaces;

namespace BusinessLogic.Services.ReadWriteServices
{
    public class FileFromDataBase
    {
        private readonly IRepository<Taxi.DAL.Models.ClientDto> _clientsRepository;

        private readonly IRepository<Taxi.DAL.Models.CarDto> _carsRepository;

        private readonly IRepository<Taxi.DAL.Models.DriverDto> _driversRepository;

        private readonly IRepository<Taxi.DAL.Models.OrderDto> _ordersRepository;

        private readonly IMapper _mapper;

        public FileFromDataBase(IRepository<Taxi.DAL.Models.ClientDto> clientsRepository, IRepository<Taxi.DAL.Models.CarDto> carsRepository, IRepository<Taxi.DAL.Models.DriverDto> driversRepository, IRepository<Taxi.DAL.Models.OrderDto> ordersRepository, IMapper mapper)
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
            var cars = _mapper.Map<IEnumerable<Models.Car>>(_carsRepository.Get());
            var clients = _mapper.Map<IEnumerable<Models.Client>>(_clientsRepository.Get());
            var drivers = _mapper.Map<IEnumerable<Models.Driver>>(_driversRepository.Get());
            var orders = _mapper.Map<IEnumerable<Models.Order>>(_ordersRepository.Get());

            //Write data
            writer.Write(cars, carsPath);
            writer.Write(clients, clientsPath);
            writer.Write(drivers, driversPath);
            writer.Write(orders, ordersPath);
        }
    }
}