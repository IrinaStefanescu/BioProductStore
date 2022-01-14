using AutoMapper;
using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.OrderRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        public BioProductStoreContext _context;
        public IOrderRepository _orderRepository;


        public OrderService(IMapper mapper, BioProductStoreContext context, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _context = context;
            _orderRepository = orderRepository;
        }


        public Order GetOrderByOrderId(Guid Id)
        {
            Order order = _orderRepository.FindById(Id);

            if (order == null)
                throw new Exception("Order not found");

            return order;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> ordersList = _orderRepository.GetAllOrders();

            if (ordersList.Count == 0)
                throw new Exception("There are no orders");

            return ordersList;
        }

        public void CreateOrder(RegisterOrderDTO entity)
        { 
            var orderToCreate = _mapper.Map<Order>(entity);
            orderToCreate.DateCreated = DateTime.Now;
            orderToCreate.DateModified = DateTime.Now;

            _orderRepository.Create(orderToCreate);
            _orderRepository.Save();
        }

        public void DeleteOrderById(Guid id)
        {
            Order order = _orderRepository.FindById(id);

            if (order == null)
                throw new Exception("Order not found");

            _orderRepository.Delete(order);
            _orderRepository.Save();
        }

       

        public void UpdateOrder(UpdateOrderDTO order, Guid id)
        {
            Order orderToUpdate = _orderRepository.FindById(id);

            if (orderToUpdate == null)
                throw new Exception("Order not found");

            orderToUpdate = _mapper.Map<UpdateOrderDTO, Order>(order, orderToUpdate);
            orderToUpdate.DateModified = DateTime.Now;

            try
            {
                _orderRepository.Update(orderToUpdate);
                _orderRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
