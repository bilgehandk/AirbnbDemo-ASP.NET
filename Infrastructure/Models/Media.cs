using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Media
    {
        [Key] 
        public int Id { get; set; }
        
        [Required] 
        public int ModelId { get; set; }

        [Required]
        public string ModelType { get; set; }
        
        [Required]
        public string FileName { get; set; }
        
        public string? MimeType { get; set; }

        // You can add a navigation property if needed, depending on the relationships
    }
}