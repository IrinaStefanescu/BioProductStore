using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.OrderRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersForAUser();
    }
}
