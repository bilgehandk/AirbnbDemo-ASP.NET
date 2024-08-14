using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Media
    {
        public int Id { get; set; }

        [Required]
        public string UrlPath { get; set; }

        public bool IsMainImage { get; set; }

        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public PropertyInfo PropertyInfo { get; set; }
    }
}