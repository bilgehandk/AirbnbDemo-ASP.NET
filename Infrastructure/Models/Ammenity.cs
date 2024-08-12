using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Ammenity
    {
        [Key]
        public int Property_Id{ get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey("AmenityType")]
        public int AmenityTypeId { get; set; }
        public AmenityType AmenityType { get; set; }
    }
}