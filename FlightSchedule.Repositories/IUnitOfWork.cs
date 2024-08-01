using FlightSchedule.EntityBase.Entity;

namespace FlightSchedule.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositories<Flight> FlightRepository { get; }
        IRepositories<Route> RouteRepository { get; }
        IRepositories<Subscription> SubscriptionRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
