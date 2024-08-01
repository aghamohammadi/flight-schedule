using FlightSchedule.EntityBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlightSchedule.EntityBase.Context
{
    public class FlightScheduleDbContext : DbContext
    {
        public FlightScheduleDbContext(DbContextOptions<FlightScheduleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flight { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Subscription> Subscription { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .Property(e => e.DepartureTime)
                .HasColumnType("datetime");

            modelBuilder.Entity<Flight>()
                .Property(e => e.ArrivalTime)
                .HasColumnType("datetime");

            modelBuilder.Entity<Route>()
                .HasIndex(r => new { r.OriginCityId, r.DestinationCityId, r.DepartureDate })
                .IsUnique();

            //modelBuilder.Entity<Subscription>()
            // .HasIndex(r => new { r.OriginCityId, r.DestinationCityId, r.AgencyId })
            // .IsUnique();

            modelBuilder.Entity<Route>()
                .HasIndex(f => f.DepartureDate)
                .HasDatabaseName("idx_route_departure_date");

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.DepartureTime)
                .HasDatabaseName("idx_flight_departure_time");

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.AirlineId)
                .HasDatabaseName("idx_flight_airline_id");

            modelBuilder.Entity<Subscription>()
                .HasIndex(f => f.AgencyId)
                .HasDatabaseName("idx_subscription_agency_id");

            modelBuilder.Entity<Subscription>()
                .HasIndex(f => f.OriginCityId)
                .HasDatabaseName("idx_subscription_origin_city_id");

            modelBuilder.Entity<Subscription>()
                .HasIndex(f => f.DestinationCityId)
                .HasDatabaseName("idx_subscription_destination_city_id");

            //new DbInitializer(modelBuilder).Seed();


            base.OnModelCreating(modelBuilder);
        }
    }

}

