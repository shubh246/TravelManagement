using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelManagement.Models
{
    public class Journey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [ForeignKey("Flight")]   
        public int FlightId { get; set; }
        [ForeignKey("Airline")]
        public int AirlineId { get; set; }
       public Flight Flight { get; set; }
       public Airline Airline { get; set; }

    }
}
