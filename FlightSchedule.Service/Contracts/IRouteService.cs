using FlightSchedule.DtoModels;

namespace FlightSchedule.Service.Contracts
{
    public interface IRouteService
    {
        Task<int[]> GetAll(DateOnly startDate, DateOnly endDate, int agencyId);
    }
}
