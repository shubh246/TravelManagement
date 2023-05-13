using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelManagement.Models.Dto
{
    public class FlightDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]+$")]
        [StringLength(50)]
        public string FlightName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        [StringLength(10)]
        public string FlightCode { get; set; }
        
    }
}
