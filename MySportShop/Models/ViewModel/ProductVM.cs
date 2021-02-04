using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<ProductInfo> ProductInfos { get; set; }
        public IEnumerable<SelectListItem> Properties { get; set; }
        public ProductInfo Info { get; set; }
    }
}
