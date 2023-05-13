using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelManagement.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]+$")]
        [StringLength(50)]
        public string FlightName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        [StringLength(10)]
        public string FlightCode { get; set; }
        [Required]
        
        [StringLength(10)] // Make sure this matches the length of AirlineCode in the Airline model
        public string AirlineCode { get; set; }
        
    }
}
