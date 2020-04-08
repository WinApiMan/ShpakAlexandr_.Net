using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Taxi.DAL.Interfaces;
using Taxi.DAL.Models;

namespace Taxi.BusinessLogic.Processings
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;

        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public bool AddOrder(OrderDto order)
        {
            var newOrder = _mapper.Map<Order>(order);
            try
            {
                _orderRepository.Create(newOrder);
                return true;
            }
            catch (DbException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteOrder(int id)
        {
            var order = _orderRepository.FindById(id);
            _orderRepository.Remove(order);
        }

        public void UpdateOrder(OrderDto order)
        {
            var newOrder = _mapper.Map<Order>(order);
            _orderRepository.Update(newOrder);
        }

        public IEnumerable<OrderDto> GetAll()
        {
            return _mapper.Map<IEnumerable<OrderDto>>(_orderRepository.Get());
        }

        public IEnumerable<OrderDto> ActiveOrders()
        {
            return _mapper.Map<IEnumerable<OrderDto>>(_orderRepository.Get(e => e.IsDone));
        }

        public IEnumerable<OrderDto> InActiveOrders()
        {
            return _mapper.Map<IEnumerable<OrderDto>>(_orderRepository.Get(e => !e.IsDone));
        }
    }
}