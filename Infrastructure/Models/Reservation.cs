using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public PropertyInfo? PropertyInfo { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        public DateTime LastUpdated { get; set; }

        public int ReservationStatusId { get; set; }

        [ForeignKey("ReservationStatusId")]
        public ReservationStatus? ReservationStatus { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}