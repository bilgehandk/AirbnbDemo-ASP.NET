using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Prices
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double Amount { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
    }
}