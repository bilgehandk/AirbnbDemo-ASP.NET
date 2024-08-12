using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class AmenityType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}