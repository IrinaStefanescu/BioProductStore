using BioProductStore.Data;
using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.OrderProductRelationshipRepository
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly BioProductStoreContext _context;

        public OrderProductRepository(BioProductStoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
