using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Reservations")]
        public int ReservationId { get; set; }
        public Reservations Reservations { get; set; }

        [Required]
        public int Rating { get; set; }
        
        [Required]
        public string Comment { get; set; }

        // Navigation property for Media, if needed
        public Media Media { get; set; }
    }
}