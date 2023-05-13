using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelManagement.Models.Dto
{
    public class JourneyCreateDTO
    {
        
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TravelDate { get; set; }
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        [StringLength(10)]
        public string FlightCode { get; set; }
        [Required]

        [StringLength(10)]
        public string AirlineCode { get; set; }
        public int FlightId { get; set; }
        public int AirlineId { get; set; }
        
    }
}
