using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelManagement.Models.Dto
{
    public class FlightCreateDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$")]
        [StringLength(50)]
        public string FlightName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        [StringLength(10)]
        public string FlightCode { get; set; }
        public string AirlineCode { get; set; }
    }
}
