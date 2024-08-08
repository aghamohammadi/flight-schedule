using FlightSchedule.DtoModels;

namespace FlightSchedule.Service.Contracts
{
    public interface IFlightService
    {
        Task<List<FlightOutputResultDto>> GetAllAsync(DateTime startDate, DateTime endDate, int[] routes);
            
    }
}
