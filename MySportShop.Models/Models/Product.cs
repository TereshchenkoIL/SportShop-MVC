using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sex { get; set; }
        public string Season { get; set; }
        public List<ProductInfo> Properties { get; set; }
        public List<OrderInfo> OrdersInfo{ get; set; }
        public string Image { get; set; }

    }
}
