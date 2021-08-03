using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySportShop.Repository.Interfaces;
using MySportShop.Repository.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Extensions
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            return services;
        }
    }
}
