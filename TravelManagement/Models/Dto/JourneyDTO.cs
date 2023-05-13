using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelManagement.Models.Dto
{
    public class JourneyDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
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
        public FlightDTO Flight { get; set; }
        public AirlineDTO Airline { get; set; }
    }
}
