using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Entity;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<FlightOutputResultDto>> GetAllAsync(DateTime startDate, DateTime endDate, int[] routes)
        {


            //make flight condition
            Expression<Func<Flight, bool>> filterFlights = f =>
                routes.Contains(f.RouteId)
                && f.DepartureTime >= startDate
                && f.DepartureTime <= endDate;

            //Get all the necessary Flights for processing
            return await _unitOfWork.FlightRepository.Get(filterFlights).Select(a => new FlightOutputResultDto()
                {
                    FlightId = a.FlightId,
                    OriginCityId = a.Route.OriginCityId,
                    DestinationCityId = a.Route.DestinationCityId,
                    DepartureTime = a.DepartureTime,
                    ArrivalTime = a.ArrivalTime,
                    AirlineId = a.AirlineId
                })
                .Distinct()
                .ToListAsync();

           
        }

        
    }
}
