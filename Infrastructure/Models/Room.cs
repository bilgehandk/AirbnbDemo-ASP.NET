using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string HomeType { get; set; }

        [Required]
        public string RoomType { get; set; } 

        [Required]
        public int TotalOccupancy { get; set; } 

        [Required]
        public int TotalBedrooms { get; set; } 

        [Required]
        public int TotalBathrooms { get; set; } 

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public bool HasTv { get; set; }

        [Required]
        public bool HasKitchen { get; set; }

        [Required]
        public bool HasAirCon { get; set; }

        [Required]
        public bool HasHeating { get; set; }

        [Required]
        public bool HasInternet { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        [ForeignKey("User")]
        public int OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }
    }
}