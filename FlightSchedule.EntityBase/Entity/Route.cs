
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FlightSchedule.EntityBase.Entity
{
    [Table("Route")]
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RouteId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateOnly DepartureDate { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
