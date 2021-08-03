using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySportShop.Data.Contexts;
using MySportShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Extensions
{
    public static class IdentityInjection
    {
        public static IServiceCollection AddIdentityRole(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>()
              .AddDefaultTokenProviders().AddDefaultUI()
              .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
