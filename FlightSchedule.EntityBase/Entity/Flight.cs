
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightSchedule.EntityBase.Entity
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FlightId { get; set; }
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirlineId { get; set; }
        public virtual Route Route { get; set; }

    }
}
