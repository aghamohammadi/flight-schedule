using System.Linq.Expressions;

namespace FlightSchedule.Repositories
{
    public interface IRepositories<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        bool IsExist(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
    }
}
