namespace Infrastructure.Models
{
    public class PropertyViewModel
    {
        public PropertyInfo PropertyInfo { get; set; } = new PropertyInfo();
        public List<Amenity> Amenities { get; set; } = new List<Amenity>();
        public List<Prices> Prices { get; set; } = new List<Prices>();
        public List<AmenityType> AmenityTypes { get; set; } = new List<AmenityType>();
        public List<Media> Media { get; set; } = new List<Media>();
        public List<Calenderavailability> CalenderAvailabilities { get; set; } = new List<Calenderavailability>();
        public List<Fee> Fees { get; set; } = new List<Fee>();
        public List<FeeType> FeeTypes { get; set; } = new List<FeeType>();
    }
}
