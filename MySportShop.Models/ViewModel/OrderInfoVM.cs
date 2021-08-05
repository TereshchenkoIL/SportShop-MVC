using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Models.ViewModel
{
    public class OrderInfoVM
    {
        public CartVM[] Products { get; set; }
        public IEnumerable<SelectListItem> Properties { get; set; }

        public CartVM this[int index]
        {
            get
            {
                if (index < 0 || index >= Products.Count())
                    throw new ArgumentOutOfRangeException();
                return Products[index];
            }
        }
    }
}
