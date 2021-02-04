using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Models
{
    public class AppUser : IdentityUser
    {
        public List<Order> Orders { get; set; }
    }
}
