using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Taxi.DAL.Interfaces;

namespace BusinessLogic.Services.ReadWriteServices
{
    public class DataBaseFromFile
    {
        private readonly IRepository<Taxi.DAL.Models.ClientDto> _clientsRepository;

        private readonly IRepository<Taxi.DAL.Models.CarDto> _carsRepository;

        private readonly IRepository<Taxi.DAL.Models.DriverDto> _driversRepository;

        private readonly IRepository<Taxi.DAL.Models.OrderDto> _orderRepository;

        private readonly IMapper _mapper;

        public DataBaseFromFile(IRepository<Taxi.DAL.Models.ClientDto> clientsRepository, IRepository<Taxi.DAL.Models.CarDto> carsRepository, IRepository<Taxi.DAL.Models.DriverDto> driversRepository, IRepository<Taxi.DAL.Models.OrderDto> orderRepository, IMapper mapper)
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
            var carsList = _mapper.Map<IEnumerable<Taxi.DAL.Models.CarDto>>(reader.Read<Models.Car>(carsPath));
            var clientsList = _mapper.Map<IEnumerable<Taxi.DAL.Models.ClientDto>>(reader.Read<Models.Client>(clientsPath));
            var driversList = _mapper.Map<IEnumerable<Taxi.DAL.Models.DriverDto>>(reader.Read<Models.Driver>(driversPath));
            var ordersList = _mapper.Map<IEnumerable<Taxi.DAL.Models.OrderDto>>(reader.Read<Models.Order>(ordersPath));

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