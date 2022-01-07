using BioProductStore.Data;
using BioProductStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly BioProductStoreContext _context;

        public CategoryRepository(BioProductStoreContext context) : base(context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return new List<Category>(_context.Categories.AsNoTracking().ToList());
        }

        public Category GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name.Equals(name));
        }
    }
}
