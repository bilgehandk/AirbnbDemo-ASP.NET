using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public float FeeAmount { get; set; }
        
        [Required]
        public string Name { get; set; }

    }
}