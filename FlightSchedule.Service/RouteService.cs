using FlightSchedule.DtoModels;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;

namespace FlightSchedule.Service
{
    public class RouteService : IRouteService
    {

        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }



        public List<RouteDto> GetAll(List<SubscriptionDto> subscriptions)
        {
            return _unitOfWork.RouteRepository.GetAll()
                .Join(subscriptions,
                          route => new { route.OriginCityId, route.DestinationCityId },
           subscription => new { subscription.OriginCityId, subscription.DestinationCityId },
            (route, subscription) => route)


                .Select(a => new RouteDto()
            {
                RouteId = a.RouteId,
                DestinationCityId = a.DestinationCityId,
                OriginCityId = a.OriginCityId
            }).ToList();
        }
    }
}
