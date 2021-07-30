using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySportShop.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task UpdateAsync(T item);
        Task AddAsync(T item);
        Task DeleteAsync(T item);
        Task<IQueryable<T>> GetAllAsync(bool tracked);
        Task<IQueryable> GetByCondition(Expression<Func<T, bool>> expression,
 bool trackChanges);
    }
}
