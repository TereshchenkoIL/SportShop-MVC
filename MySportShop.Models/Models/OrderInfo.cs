using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.Models
{
    public class OrderInfo
    {
        
        public int OrderId { get; set; }
   
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double Size { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
