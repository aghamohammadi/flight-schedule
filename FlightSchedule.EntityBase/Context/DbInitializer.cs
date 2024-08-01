using CsvHelper;
using CsvHelper.Configuration;
using FlightSchedule.EntityBase.Entity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace FlightSchedule.EntityBase.Context
{
    public class DbInitializer
    {
        private readonly FlightScheduleDbContext _context;
        private readonly ModelBuilder _modelBuilder;

        
        public DbInitializer(FlightScheduleDbContext context)
        {
            _context = context;
        }

        //public void Seed()
        //{

        //    var currentDirectory = Directory.GetCurrentDirectory();
        //    var directoryInfo = new DirectoryInfo(currentDirectory);
        //    var parentDirectory = directoryInfo.Parent.Parent.Parent.Parent;

        //    string filePathSubscription = Path.Combine(parentDirectory.FullName, "FlightSchedule.EntityBase", "SeedData", "subscriptions.csv");

        //    using (var reader = new StreamReader(filePathSubscription))
        //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //    {
        //        csv.Context.RegisterClassMap<SubscriptionMap>();

        //        var records = csv.GetRecords<Subscription>().ToList();

        //        _context.Subscription.AddRange(records);
        //        _context.SaveChanges();
        //    }



        //    //modelBuilder.Entity<User>().HasData(
        //    //       new User() { Id = 1.... },
        //    //       new User() { Id = 2.... },


        //    //);
        //}

        public void Seed()
        {

            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryInfo = new DirectoryInfo(currentDirectory);
            var parentDirectory = directoryInfo.Parent.Parent.Parent.Parent;

            string filePathSubscription = Path.Combine(parentDirectory.FullName, "FlightSchedule.EntityBase", "SeedData", "subscriptions.csv");

            using (var reader = new StreamReader(filePathSubscription))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<SubscriptionMap>();

                var records = csv.GetRecords<Subscription>().ToList();
                //records.ForEach(a => a.Id = Guid.NewGuid());
                //_modelBuilder.Entity<Subscription>().HasData(records);
                _context.Subscription.AddRange(records);
                _context.SaveChanges();

            }


            string filePathRoutes = Path.Combine(parentDirectory.FullName, "FlightSchedule.EntityBase", "SeedData", "routes.csv");

            using (var reader = new StreamReader(filePathRoutes))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RouteMap>();

                var records = csv.GetRecords<Route>().ToList();
                //_modelBuilder.Entity<Route>().HasData(records);
                _context.Route.AddRange(records);
                _context.SaveChanges();

            }


            string filePathFlights = Path.Combine(parentDirectory.FullName, "FlightSchedule.EntityBase", "SeedData", "flights.csv");

            using (var reader = new StreamReader(filePathFlights))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<FlightMap>();

                var records = csv.GetRecords<Flight>().ToList();
                //_modelBuilder.Entity<Flight>().HasData(records);
                _context.Flight.AddRange(records);
                _context.SaveChanges();
            }



        }
    }

    public class SubscriptionMap : ClassMap<Subscription>
    {
        public SubscriptionMap()
        {
            Map(m => m.AgencyId).Name("agency_id");
            Map(m => m.OriginCityId).Name("origin_city_id");
            Map(m => m.DestinationCityId).Name("destination_city_id");
        }
    }

    public class RouteMap : ClassMap<Route>
    {
        public RouteMap()
        {
            Map(m => m.RouteId).Name("route_id");
            Map(m => m.OriginCityId).Name("origin_city_id");
            Map(m => m.DestinationCityId).Name("destination_city_id");
            Map(m => m.DepartureDate).Name("departure_date");

        }
    }

    public class FlightMap : ClassMap<Flight>
    {
        public FlightMap()
        {
            Map(m => m.FlightId).Name("flight_id");
            Map(m => m.RouteId).Name("route_id");
            Map(m => m.DepartureTime).Name("departure_time");
            Map(m => m.ArrivalTime).Name("arrival_time");
            Map(m => m.AirlineId).Name("airline_id");


        }
    }

}
