using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public int AmenityTypeId { get; set; }

        [ForeignKey("AmenityTypeId")]
        public AmenityType AmenityType { get; set; }

        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public PropertyInfo PropertyInfo { get; set; }
    }
}