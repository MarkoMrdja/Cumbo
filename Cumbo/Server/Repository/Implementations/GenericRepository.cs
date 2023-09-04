using Cumbo.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cumbo.Server.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return true;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return true;
        }

        public virtual async Task<bool> RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return true;
        }

        public virtual async Task<bool> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return true;
        }

        public virtual async Task<bool> UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return true;
        }
    }
}
