using BioProductStore.Data;
using BioProductStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly BioProductStoreContext _context;

        public ProductRepository(BioProductStoreContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(_context.Products.AsNoTracking().ToList());
        }

        public Product GetByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name.Equals(name));
        }

    }
}
