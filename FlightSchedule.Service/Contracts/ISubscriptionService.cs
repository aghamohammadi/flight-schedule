using FlightSchedule.DtoModels;

namespace FlightSchedule.Service.Contracts
{
    public interface ISubscriptionService
    {
        List<SubscriptionDto> GetAll(int agencyId);
    }
}
