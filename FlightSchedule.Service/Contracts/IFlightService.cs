using FlightSchedule.DtoModels;

namespace FlightSchedule.Service.Contracts
{
    public interface IFlightService
    {
        List<FlightOutputResultDto> DetectChanges(DateTime startDate, DateTime endDate, int agencyId);
        
    }
}
