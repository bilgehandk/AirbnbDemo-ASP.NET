using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}