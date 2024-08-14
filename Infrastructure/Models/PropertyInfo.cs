using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PropertyInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PropertyName { get; set; }

        [Required]
        public string PropertyType { get; set; }

        public int TotalOccupancy { get; set; }
        public int TotalBedrooms { get; set; }
        public int TotalBathrooms { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public DateTime LastUpdated { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ImageUrl { get; set; }
        public string SecondImageUrl { get; set; }

        public double Prices { get; set; }

        [Required]
        [ForeignKey("Amenity")]
        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }

        [Required]
        [ForeignKey("Fee")]
        public int FeeId { get; set; }
        public Fee Fee { get; set; }
        
    }
}