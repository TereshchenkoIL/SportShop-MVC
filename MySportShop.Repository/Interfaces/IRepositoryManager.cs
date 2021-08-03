using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Interfaces
{
    interface IRepositoryManager
    {
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        IPropertyRepository Property { get; }
        Task Save();
    }
}
