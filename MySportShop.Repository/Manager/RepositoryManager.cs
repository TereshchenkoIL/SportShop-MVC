using MySportShop.Data.Contexts;
using MySportShop.Repository.ConcreteRepositories;
using MySportShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Manager
{
    class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _db;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IPropertyRepository _propertyRepository;

        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_db);
                return _orderRepository;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_db);
                return _productRepository;
            }
        }

        public IPropertyRepository Property
        {
            get
            {
                if (_propertyRepository == null)
                    _propertyRepository = new PropertyRepository(_db);
                return _propertyRepository;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
