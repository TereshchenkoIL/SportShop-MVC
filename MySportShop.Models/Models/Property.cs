using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models.Models
{
    public class Property
    {
        [Key]
        public double Size { get; set; }
        public string Length { get; set; }
        public List<ProductInfo> PropInfo { get; set; }
    }
}
