using Microsoft.AspNetCore.Http;
using MySportShop.Models.Models;
using MySportShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop
{
    public static class WC
    {
        public const string ImagePath = @"\images\product\";
        public const string SessionCart = "ShoppingCartSession";

        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";

        public const string EmailAdmin = "ben.spark90@yahoo.com";

        public static IEnumerable<ShoppingCart> GetCartItems(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            List<ShoppingCart> items = new List<ShoppingCart>();
            if (httpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
               && httpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                items = httpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            return items;
        }

    }
}
