using System.Linq.Expressions;

namespace FlightSchedule.Repositories
{
    public interface IRepositories<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void RemoveAsync(T entity);
    }
}
