using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public IEnumerable<Amenity>? Amenities { get; set; }
        public IEnumerable<Media>? MediaItems { get; set; }
        public IEnumerable<Prices>? Prices { get; set; }
    }
}