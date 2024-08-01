using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FlightSchedule.EntityBase.Context;

namespace FlightSchedule.Repositories
{
    public class Repository<T> : IRepositories<T> where T : class
    {
        protected readonly FlightScheduleDbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(FlightScheduleDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking();            
            
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {

            return DbSet.Any(predicate);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
