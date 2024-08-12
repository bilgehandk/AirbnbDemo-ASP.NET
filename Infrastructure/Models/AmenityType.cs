using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class AmenityType
    {
        [Key]
        public int AmneityTypeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Ammenity> Ammenities { get; set; }
    }
}