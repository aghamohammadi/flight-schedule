using static FlightSchedule.DtoModels.Enums;

namespace FlightSchedule.DtoModels
{
    public class FlightOutputResultDto
    {
        public long FlightId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirlineId { get; set; }
        public FlightStatus Status { get; set; }
        
    }
}
