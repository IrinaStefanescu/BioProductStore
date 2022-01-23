using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BioProductStore.DataAccess;
using BioProductStore.DTOs;
using BioProductStore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BioProductStore.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private UnitOfWork _uow;
        
        public OrderService(IMapper mapper, UnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }


        public Order GetOrderByOrderId(Guid Id)
        {
            Order order = _uow.Order.FindById(Id);

            if (order == null)
                throw new Exception("Order not found");

            return order;
        }

        public List<Order> GetAllOrders()
        {
            var ordersList = _uow.Order.GetAllAsQueryable();

            if (ordersList.Count() == 0)
                throw new Exception("There are no orders");

            return ordersList.ToList();
        }

        public List<Order> GetAllOrdersForAUser(Guid userId)
        {
            return _uow.Order
                .GetAllAsQueryable()
                .Include(o => o.User)
                .Where(o => o.User.Id == userId)
                .ToList();
        }

        public void CreateOrder(RegisterOrderDTO entity)
        { 
            var orderToCreate = _mapper.Map<Order>(entity);
            orderToCreate.DateCreated = DateTime.Now;
            orderToCreate.DateModified = DateTime.Now;

            _uow.Order.Create(orderToCreate);
            _uow.SaveChanges();
        }

        public void DeleteOrderById(Guid id)
        {
            Order order = _uow.Order.FindById(id);

            if (order == null)
                throw new Exception("Order not found");

            _uow.Order.Delete(order);
            _uow.SaveChanges();
        }

       

        public void UpdateOrder(UpdateOrderDTO order, Guid id)
        {
            Order orderToUpdate = _uow.Order.FindById(id);

            if (orderToUpdate == null)
                throw new Exception("Order not found");

            orderToUpdate = _mapper.Map<UpdateOrderDTO, Order>(order, orderToUpdate);
            orderToUpdate.DateModified = DateTime.Now;

            try
            {
                _uow.Order.Update(orderToUpdate);
                _uow.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
