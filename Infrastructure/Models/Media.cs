using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        [MaxLength(255)]
        public string UrlPath { get; set; }

        [Required]
        public bool IsMainImage { get; set; }
    }
}