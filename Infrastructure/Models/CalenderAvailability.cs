using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class CalenderAvailability
    {
        [Key]
        public int CalenderId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}