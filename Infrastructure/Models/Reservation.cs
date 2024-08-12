using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [ForeignKey("ReservationStatus")]
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}