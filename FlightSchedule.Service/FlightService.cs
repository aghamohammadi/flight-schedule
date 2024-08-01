using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Entity;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
using System.Linq.Expressions;
using static FlightSchedule.DtoModels.Enums;

namespace FlightSchedule.Service
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlightService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<FlightOutputResultDto> DetectChanges(DateTime startDate, DateTime endDate, int agencyId)
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
            var routes = _unitOfWork.RouteRepository.GetAll()
                .Where(route => route.DepartureDate >= allDataStartDate && route.DepartureDate <= allDataEndDate)
                .Join(_unitOfWork.SubscriptionRepository.GetAll().Where(s => s.AgencyId == agencyId),
                    route => new { route.OriginCityId, route.DestinationCityId },
                    subscription => new { subscription.OriginCityId, subscription.DestinationCityId },
                    (route, subscription) => route.RouteId)
                .Distinct()
                .ToList();

           

            if (routes == null || routes.Count == 0)
                throw new Exception("No Route Found");

            //make flight condition
            Expression<Func<Flight, bool>> filterFlights = f =>
                routes.Contains(f.RouteId)
                && f.DepartureTime >= allDataStartDateTime
                && f.DepartureTime <= allDataEndDateTime;

            //Get all the necessary Flights for processing
            var allDataFlights = _unitOfWork.FlightRepository.Get(filterFlights).Select(a => new FlightOutputResultDto()
                {
                    FlightId = a.FlightId,
                    OriginCityId = a.Route.OriginCityId,
                    DestinationCityId = a.Route.DestinationCityId,
                    DepartureTime = a.DepartureTime,
                    ArrivalTime = a.ArrivalTime,
                    AirlineId = a.AirlineId
                })
                .Distinct()
                .ToList();

            if (allDataFlights == null || allDataFlights.Count == 0)
                throw new Exception("No Flight Found");

            //Get all the Flights in real date range 
            var realFlights = allDataFlights.Where(a => a.DepartureTime >= startDate && a.DepartureTime <= endDate)
                .ToList();

            if (realFlights == null || realFlights.Count == 0)
                throw new Exception("No Flight Found");

            //detect change for each flights
            realFlights.ForEach(a => a.Status = ChangeDetection(allDataFlights, a.AirlineId, a.DepartureTime));



            return realFlights;
        }

        //get change detection for the flight of the airline
        private FlightStatus ChangeDetection(List<FlightOutputResultDto> allDataFlights, int airlineId,
            DateTime departureTime)
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
