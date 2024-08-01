
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FlightSchedule.EntityBase.Entity
{
    [Table("Subscription")]
    [Keyless]
    public class Subscription
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public Guid Id { get; set; }
        public int AgencyId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
      
    }
}
