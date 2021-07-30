using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.Models.ViewModel
{
    public class CartVM
    {
        public CartVM(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product;
        public int Quantity;
    }
}
