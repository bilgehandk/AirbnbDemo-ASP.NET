using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class ReservationStatus
    {
        [Key]
        public uint ReservationStatusId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Type { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}