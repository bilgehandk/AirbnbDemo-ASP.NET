using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string PropertyType { get; set; }

        [Required]
        public int TotalOccupancy { get; set; }

        [Required]
        public int TotalBedrooms { get; set; }

        [Required]
        public int TotalBathrooms { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        public string City { get; set; }

        [Required]
        [MaxLength(255)]
        public string State { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }

        public ICollection<Ammenity> Ammenities { get; set; }
        public ICollection<CalenderAvaliability> CalendarAvailabilities { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Prices> Prices { get; set; }
    }
}