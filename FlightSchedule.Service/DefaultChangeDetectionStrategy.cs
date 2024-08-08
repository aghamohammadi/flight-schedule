using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Entity;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static FlightSchedule.DtoModels.Enums;

namespace FlightSchedule.Service
{
    public class DefaultChangeDetectionStrategy : IChangeDetectionStrategy
    {

        private readonly IFlightService _flightService;
        private readonly IRouteService _routeService;


        public DefaultChangeDetectionStrategy(IFlightService flightService, IRouteService routeService)
        {
            _flightService = flightService;
            _routeService = routeService;
        }
        public async Task<List<FlightOutputResultDto>> DetectChanges(DateTime startDate, DateTime endDate, int agencyId)
        {
            //Determine the start of the range to receive all the necessary data for processing
            var allDataStartDateTime = startDate.AddDays(-7).AddMinutes(-30);
            //Determine the end of the range to receive all the necessary data for processing
            var allDataEndDateTime = endDate.AddDays(+7).AddMinutes(+30);

            //Get DateOnly the start of the range 
            var allDataStartDate = DateOnly.FromDateTime(allDataStartDateTime);
            //Get DateOnly the end of the range 
            var allDataEndDate = DateOnly.FromDateTime(allDataEndDateTime);


            //Get routes of agencyId in date range 
            var routes =await _routeService.GetAll(allDataStartDate, allDataEndDate,agencyId);



            if (routes == null || routes.Length == 0)
                throw new Exception("No Route Found");


            //Get all the necessary Flights for processing
            var allDataFlights =await _flightService.GetAllAsync(allDataStartDateTime, allDataEndDateTime, routes);

            if (allDataFlights == null || allDataFlights.Count == 0)
                throw new Exception("No Flight Found");

            //Get all the Flights in real date range 
            var realFlights = allDataFlights.Where(a => a.DepartureTime >= startDate && a.DepartureTime <= endDate)
                .ToList();

            if (realFlights == null || realFlights.Count == 0)
                throw new Exception("No Flight Found");

            //detect change for each flights
            realFlights.ForEach(a => a.Status = DetectStatus(allDataFlights, a.AirlineId, a.DepartureTime));



            return realFlights;
        }
        private FlightStatus DetectStatus(List<FlightOutputResultDto> allDataFlights, int airlineId, DateTime departureTime)
        {
            //Determine the start of the range For New flight Status
            var startDateNewFlight = departureTime.AddDays(-7).AddMinutes(-30);
            //Determine the end of the range For New flight Status
            var endDateNewFlight = departureTime.AddDays(-7).AddMinutes(+30);


            //get New status
            var newFlights = allDataFlights.Any(w =>
                w.AirlineId == airlineId && w.DepartureTime >= startDateNewFlight &&
                w.DepartureTime <= endDateNewFlight);
            if (newFlights)
                return FlightStatus.New;

            //Determine the start of the range For Discontinued flight Status
            var startDateDiscontinuedFlight = departureTime.AddDays(7).AddMinutes(-30);
            //Determine the end of the range For Discontinued flight Status
            var endDateDiscontinuedFlight = departureTime.AddDays(7).AddMinutes(+30);

            //get Discontinued status
            var discontinuedFlights = allDataFlights.Any(w =>
                w.AirlineId == airlineId && w.DepartureTime >= startDateDiscontinuedFlight &&
                w.DepartureTime <= endDateDiscontinuedFlight);
            if (discontinuedFlights)
                return FlightStatus.Discontinued;

            return FlightStatus.Unknown;
        }
    }

}
