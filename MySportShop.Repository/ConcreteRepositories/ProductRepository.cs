using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Product>> GetAllWithProps(bool trackedChanges) => trackedChanges ?
                                                                                await _db.Set<Product>()
                                                                                .Include(x => x.Properties).ToListAsync() :
                                                                                await _db.Set<Product>()
                                                                                .Include(x => x.Properties)
                                                                                .AsNoTracking().ToListAsync();



        public async Task<Product> GetById(int? id, bool trackedChanges)
        {
            var products = await GetByCondition(x => x.ProductId == id,trackedChanges);
            return products.FirstOrDefault();
        }

        public async Task<List<ProductInfo>> GetProductInfos(int? id, bool trackedChanges) => trackedChanges ?
                                                                                await _db.Set<ProductInfo>().Where(x => x.ProductId == id)
                                                                                .ToListAsync() :
                                                                                 await _db.Set<ProductInfo>().Where(x => x.ProductId == id)
                                                                                .AsNoTracking().ToListAsync();



    }
}
