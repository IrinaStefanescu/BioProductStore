using BioProductStore.Data;
using BioProductStore.Models;
using BioProductStore.Repositories;

namespace BioProductStore.DataAccess
{
    public class UnitOfWork
    {
        private readonly BioProductStoreContext _context;

        public UnitOfWork(
            BioProductStoreContext context,
            IGenericRepository<Category> categoryRepository,
            IGenericRepository<ExpeditionAddress> expeditionAddressRepository,
            IGenericRepository<Order> orderRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<User> userRepository
        )
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _expeditionAddressRepository = expeditionAddressRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        private readonly IGenericRepository<Category> _categoryRepository;
        public IGenericRepository<Category> Category => _categoryRepository;

        private readonly IGenericRepository<ExpeditionAddress> _expeditionAddressRepository;
        public IGenericRepository<ExpeditionAddress> ExpeditionAddress => _expeditionAddressRepository;

        private readonly IGenericRepository<Order> _orderRepository;
        public IGenericRepository<Order> Order => _orderRepository;

        private readonly IGenericRepository<Product> _productRepository;
        public IGenericRepository<Product> Product => _productRepository;

        private readonly IGenericRepository<User> _userRepository;
        public IGenericRepository<User> User => _userRepository;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}