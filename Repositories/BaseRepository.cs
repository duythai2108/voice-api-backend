using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoiceAPI.DbContextVoiceAPI;
using VoiceAPI.IRepository;

namespace VoiceAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly VoiceAPIDbContext _context;

        public BaseRepository(VoiceAPIDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetById<TKey>(TKey id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetsAsync(Expression<Func<T, bool>> filter = null,
                                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                      string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual void Create(T type)
        {
            dbSet.Add(type);
        }

        public virtual void Update(T type)
        {
            dbSet.Update(type);
        }

        public virtual void Delete(T type)
        {
            dbSet.Remove(type);
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> Get()
        {
            return this.dbSet;
        }
    }
}
