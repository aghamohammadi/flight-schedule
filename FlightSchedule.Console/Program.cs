using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Context;
using FlightSchedule.Repositories;
using FlightSchedule.Service;
using FlightSchedule.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: YourProgram.exe <start_date> <end_date> <agency_id>");
                return;
            }

            #region Configs

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContextPool<FlightScheduleDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection")));
            builder.Services.AddLogging(b =>
            {
                b.ClearProviders();
                b.SetMinimumLevel(LogLevel.None);
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IRouteService, RouteService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IChangeDetectionStrategyFactory, ChangeDetectionStrategyFactory>(); 

            var app = builder.Build();

            //Import Data from csv files
            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<FlightScheduleDbContext>();
            //    new DbInitializer(context).Seed();
            //}

            #endregion


            //DateTime startDate = DateTime.Parse("2018-01-01");
            //DateTime endDate = DateTime.Parse("2018-01-15");
            //int agencyId = int.Parse("1");


            DateTime startDate, endDate;
            int agencyId;

            if (!DateTime.TryParse(args[0], out startDate) ||
                !DateTime.TryParse(args[1], out endDate) ||
                !int.TryParse(args[2], out agencyId))
            {
                Console.WriteLine("Invalid input. Please ensure the dates are in yyyy-mm-dd format and agency_id is an integer.");
                return;
            }

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;


                //using factory design pattern 
                var strategyFactory = services.GetRequiredService<IChangeDetectionStrategyFactory>();
                var strategy1 = strategyFactory.CreateStrategy("Strategy1");


                System.Console.WriteLine($"Start Detection...");

                var stopwatch = Stopwatch.StartNew();

                var results = await strategy1.DetectChanges(startDate, endDate, agencyId);

                stopwatch.Stop();
                var elapsedMs = stopwatch.ElapsedMilliseconds;

                // Output metrics
                System.Console.WriteLine($"Detection completed in {elapsedMs} ms");
                System.Console.WriteLine($"Total Flights detected: {results.Count}");

                WriteChangesToCsv(results, "results.csv");

            }

           


        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");

        }


        void WriteChangesToCsv(List<FlightOutputResultDto> results, string filePath)
        {
            var records = results.Select(c => new FlightOutputResultDto
            {
                FlightId = c.FlightId,
                OriginCityId = c.OriginCityId,
                DestinationCityId = c.DestinationCityId,
                DepartureTime = c.DepartureTime,
                ArrivalTime = c.ArrivalTime,
                AirlineId = c.AirlineId,
                Status = c.Status
            }).ToList();

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }


    }


}