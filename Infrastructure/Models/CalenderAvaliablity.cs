using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class CalenderAvaliability
    {
        [Key]
        public uint CalenderId { get; set; }

        [ForeignKey("Property")]
        public long PropertyId { get; set; }
        public Property Property { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}