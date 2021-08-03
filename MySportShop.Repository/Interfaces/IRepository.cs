using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Update(T item);
        Task AddAsync(T item);
        void Delete(T item);
        Task<List<T>> GetAllAsync(bool tracked);
        Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression,
 bool trackChanges);
    }
}
