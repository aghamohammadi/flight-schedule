using FlightSchedule.DtoModels;

namespace FlightSchedule.Service.Contracts
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionDto>> GetAllAsync(int agencyId);
    }
}
