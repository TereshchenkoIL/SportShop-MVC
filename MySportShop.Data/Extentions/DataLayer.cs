using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySportShop.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Data.Extentions
{
    public static class DataLayer
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services,
            IConfiguration configuration, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
       
            return services;
        }
    }
}
