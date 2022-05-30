using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VoiceAPI.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById<TKey>(TKey id);

        IQueryable<T> Get();
        Task<IEnumerable<T>> GetsAsync(Expression<Func<T, bool>> filter = null,
                                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                      string includeProperties = "");
        void Create(T type);
        void Update(T type);
        void Delete(T type);
        Task<int> SaveAsync();
    }
}
