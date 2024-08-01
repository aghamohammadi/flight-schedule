namespace FlightSchedule.DtoModels
{
    public class RouteDto
    {
        public int RouteId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateOnly DepartureDate { get; set; }
    }
}
