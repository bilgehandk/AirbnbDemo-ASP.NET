using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Prices
    {
        [Key]
        public uint Property_ID { get; set; }

        [ForeignKey("Property")]
        public int PropertyID { get; set; }
        public Property Property { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public float PriceAmount { get; set; }
    }
}