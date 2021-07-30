using MySportShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySportShop.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MySportShop.Repository.RepositoryBase
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public RepositoryBase(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(T item)
        {
            await _db.Set<T>().AddAsync(item);
        }

        public  void Delete(T item)
        {
             _db.Set<T>().Remove(item);
        }

        public async Task<IQueryable<T>> GetAllAsync(bool trackChanges) =>   !trackChanges ?
                                                                            _db.Set<T>()
                                                                            .AsNoTracking() :
                                                                            _db.Set<T>();

        public async Task<IQueryable<T>> GetByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>   !trackChanges ?
                                    _db.Set<T>().Where(expression)
                                    .AsNoTracking() :
                                    _db.Set<T>().Where(expression);



        public void Update(T item)
        {
            _db.Update(item);
        }
    }
}
