using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models
{
    public class ProductInfo
    {
        [Key]
        public  int InfoId { get; set; }
        public int ProductId { get; set; }
      
        public string Size { get; set; }
   
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Property Property { get; set; }
    }
}
