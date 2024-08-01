using FlightSchedule.DtoModels;

namespace FlightSchedule.Service.Contracts
{
    public interface IRouteService
    {
        List<RouteDto> GetAll(List<SubscriptionDto> subscriptions);
    }
}
