using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FlightSchedule.EntityBase.Context;

namespace FlightSchedule.Repositories
{
    public class Repository<T> : IRepositories<T> where T : class
    {
        protected readonly FlightScheduleDbContext DbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(FlightScheduleDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();            
            
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {

            return await _dbSet.AnyAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
