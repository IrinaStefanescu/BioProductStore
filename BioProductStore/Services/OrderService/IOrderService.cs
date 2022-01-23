using BioProductStore.DTOs;
using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.OrderService
{
    public interface IOrderService
    {
        Order GetOrderByOrderId(Guid Id);
        public List<Order> GetAllOrders();
        public List<Order> GetAllOrdersForAUser(Guid userId);
        void CreateOrder(RegisterOrderDTO entity);
        void DeleteOrderById(Guid id);
        void UpdateOrder(UpdateOrderDTO order, Guid id);
    }
}
