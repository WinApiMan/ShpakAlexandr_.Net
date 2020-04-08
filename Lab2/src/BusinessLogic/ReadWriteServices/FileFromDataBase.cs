using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace BusinessLogic.Services.ReadWriteServices
{
    public class FileFromDataBase
    {
        private readonly IRepository<Client> _clientsRepository;

        private readonly IRepository<Car> _carsRepository;

        private readonly IRepository<Driver> _driversRepository;

        private readonly IRepository<Order> _orderRepository;

        private readonly IMapper _mapper;

        public FileFromDataBase(IRepository<Client> clientsRepository, IRepository<Car> carsRepository, IRepository<Driver> driversRepository, IRepository<Order> orderRepository, IMapper mapper)
        {
            _clientsRepository = clientsRepository;
            _carsRepository = carsRepository;
            _driversRepository = driversRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public void ImportAndExportData(IReader reader, string carsPath, string clientsPath, string driversPath, string ordersPath)
        {
            //Read data
            var carsList = _mapper.Map<IEnumerable<Car>>(reader.Read<CarDto>(carsPath));
            var clientsList = _mapper.Map<IEnumerable<Client>>(reader.Read<ClientDto>(clientsPath));
            var driversList = _mapper.Map<IEnumerable<Driver>>(reader.Read<DriverDto>(driversPath));
            var ordersList = _mapper.Map<IEnumerable<Order>>(reader.Read<OrderDto>(ordersPath));

            //Write data
            try
            {
                _carsRepository.AddRange(carsList);
                _clientsRepository.AddRange(clientsList);
                _driversRepository.AddRange(driversList);
                _orderRepository.AddRange(ordersList);
            }
            catch (DbUpdateException message)
            {
                Console.WriteLine(message.Message);
            }
            catch (Exception message)
            {
                Console.WriteLine(message.Message);
            }
        }
    }
}