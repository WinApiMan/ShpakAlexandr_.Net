﻿using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.BusinessLogic.Interfaces;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderDto> _orderRepository;

        private readonly IMapper _mapper;

        public OrderService(IRepository<OrderDto> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task Add(Order order)
        {
            var newOrder = _mapper.Map<OrderDto>(order);
            await _orderRepository.Create(newOrder);
        }

        public async Task Delete(int id)
        {
            var order = await _orderRepository.FindById(id);
            await _orderRepository.Remove(order);
        }

        public async Task Update(Order order)
        {
            var newOrder = _mapper.Map<OrderDto>(order);
            await _orderRepository.Update(newOrder);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return _mapper.Map<IEnumerable<Order>>(await _orderRepository.Get());
        }

        public async Task<IEnumerable<Order>> GetActiveOrders()
        {
            var orders = await _orderRepository.Get();
            return _mapper.Map<IEnumerable<Order>>(orders.Where(e => e.IsDone));
        }

        public async Task<IEnumerable<Order>> GetInActiveOrders()
        {
            var orders = await _orderRepository.Get();
            return _mapper.Map<IEnumerable<Order>>(orders.Where(e => !e.IsDone));
        }

        public async Task<Order> FindById(int id)
        {
            if (id > 0)
            {
                return _mapper.Map<Order>(await _orderRepository.FindById(id));
            }
            else
            {
                throw new ArgumentException("Id less then zero");
            }
        }
    }
}