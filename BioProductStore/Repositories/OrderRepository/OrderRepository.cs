using BioProductStore.Data;
using BioProductStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly BioProductStoreContext _context;

        public OrderRepository(BioProductStoreContext context) : base(context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            return new List<Order>(_context.Orders.AsNoTracking().ToList());
        }
    }
}
