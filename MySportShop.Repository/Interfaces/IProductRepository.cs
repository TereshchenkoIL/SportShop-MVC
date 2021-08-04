using MySportShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetById(int? id,bool trackedChanges);
        Task<List<Product>> GetAllWithProps(bool trackedChanges);

        Task<List<ProductInfo>> GetProductInfos(int? id, bool trackedChanges);



    }
}
