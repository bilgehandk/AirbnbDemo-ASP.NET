using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class PropertyInfo
    {
        public int Id { get; set; }

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
        [Display(Name = "Amenity")]
        public int AmenityId { get; set; }
        [Required]
        [Display(Name = "Fee")]
        public int FeeId { get; set; }
        [Required]
        [Display(Name = "CalenderAvailability")]
        public int CalenderAvailabilityId { get; set; }

        /// <summary>
        /// These are the two other objects I'm relating, linking the PK to the FK
        /// </summary>

        [ForeignKey("AmenityId")]
        public Amenity? Amenity { get; set; }

        [ForeignKey("FeeId")]
        public Fee? Fee { get; set; }
        
        [ForeignKey("CalenderAvailabilityId")]
        public CalenderAvailability? CalenderAvailability { get; set; }
    }
}