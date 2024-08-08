using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Entity;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FlightSchedule.Service
{
    public class RouteService : IRouteService
    {

        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }



        public async Task<int[]> GetAll(DateOnly startDate, DateOnly endDate, int agencyId)
        {
  

            return await _unitOfWork.RouteRepository.GetAll()
                .Where(route => route.DepartureDate >= startDate && route.DepartureDate <= endDate)
                .Join(_unitOfWork.SubscriptionRepository.GetAll().Where(s => s.AgencyId == agencyId),
                    route => new { route.OriginCityId, route.DestinationCityId },
                    subscription => new { subscription.OriginCityId, subscription.DestinationCityId },
                    (route, subscription) => route.RouteId)
                .Distinct()
                .ToArrayAsync();
        }
    }
}
