

namespace FlightSchedule.DtoModels
{
    public class FlightDto
    {
        public long FlightId { get; set; }
        public int RouteId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirlineId { get; set; }
    }
}
