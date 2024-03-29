﻿using MySportShop.Data.Contexts;
using MySportShop.Repository.ConcreteRepositories;
using MySportShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Manager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _db;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IPropertyRepository _propertyRepository;
        private IUserRepository _userRepository;

        public RepositoryManager(ApplicationDbContext db)
        {
            _db = db;
        }

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

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_db);
                return _userRepository;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
