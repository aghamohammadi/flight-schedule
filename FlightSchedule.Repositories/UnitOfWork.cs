using FlightSchedule.EntityBase.Context;
using FlightSchedule.EntityBase.Entity;


namespace FlightSchedule.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlightScheduleDbContext _dbContext;

        public UnitOfWork(FlightScheduleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IRepositories<Flight> _flightRepository;
        public IRepositories<Flight> FlightRepository
        {
            get
            {
                return _flightRepository ??= new Repository<Flight>(_dbContext);
            }
        }

        private IRepositories<Route> _routeRepository;
        public IRepositories<Route> RouteRepository
        {
            get
            {
                return _routeRepository ??= new Repository<Route>(_dbContext);
            }
        }



        private IRepositories<Subscription> _subscriptionRepository;
        public IRepositories<Subscription> SubscriptionRepository
        {
            get
            {
                return _subscriptionRepository ??= new Repository<Subscription>(_dbContext);
            }
        }

      

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            _dbContext.Dispose();

        }
    }
}
