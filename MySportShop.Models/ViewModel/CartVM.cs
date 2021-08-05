using MySportShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.ViewModel
{
    public class CartVM
    {
        public CartVM(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Size { get; set; }
    }
}
