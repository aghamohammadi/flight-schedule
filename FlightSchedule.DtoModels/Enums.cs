
using System.ComponentModel.DataAnnotations;


namespace FlightSchedule.DtoModels
{
    public class Enums
    {
        public enum FlightStatus 
        {
            [Display(Name = "Unknown")]
            Unknown = 0,
            [Display(Name = "Discontinued")]
            Discontinued = 1,
            [Display(Name = "New")]
            New = 2
                
            
        }
    }
}
