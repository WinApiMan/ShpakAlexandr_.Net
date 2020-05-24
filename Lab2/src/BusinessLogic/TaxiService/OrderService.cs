﻿using AutoMapper;
using BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Processings
{
    public class OrderService
    {
        private readonly IRepository<OrderDto> _orderRepository;

        private readonly IMapper _mapper;

        public OrderService(IRepository<OrderDto> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task AddOrder(Order order)
        {
            var newOrder = _mapper.Map<OrderDto>(order);
            await _orderRepository.Create(newOrder);
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _orderRepository.FindById(id);
            await _orderRepository.Remove(order);
        }

        public async Task UpdateOrder(Order order)
        {
            var newOrder = _mapper.Map<OrderDto>(order);
            await _orderRepository.Update(newOrder);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return _mapper.Map<IEnumerable<Order>>(await _orderRepository.Get());
        }

        public async Task<IEnumerable<Order>> ActiveOrders()
        {
            var orders = await _orderRepository.Get();
            return _mapper.Map<IEnumerable<Order>>(orders.Where(e => e.IsDone));
        }

        public async Task<IEnumerable<Order>> InActiveOrders()
        {
            var orders = await _orderRepository.Get();
            return _mapper.Map<IEnumerable<Order>>(orders.Where(e => !e.IsDone));
        }
    }
}