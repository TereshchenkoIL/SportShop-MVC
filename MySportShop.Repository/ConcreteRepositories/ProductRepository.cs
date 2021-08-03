using MySportShop.Data.Contexts;
using MySportShop.Models.Models;
using MySportShop.Repository.Interfaces;
using MySportShop.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.ConcreteRepositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Product> GetById(int id, bool trackedChanges)
        {
            var products = await GetByCondition(x => x.ProductId == id,trackedChanges);
            return products.FirstOrDefault();
        }
    }
}
