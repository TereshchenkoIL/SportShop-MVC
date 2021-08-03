using MySportShop.Data.Contexts;
using MySportShop.Models.Models;
using MySportShop.Repository.Interfaces;
using MySportShop.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.ConcreteRepositories
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db)
        {

        }
    }
}
