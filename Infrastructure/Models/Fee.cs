using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Fee
    {
        [Key]
        public int Property_Id { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        [Required]
        public float FeeAmount { get; set; }

        [ForeignKey("FeeType")]
        public int FeeTypeId { get; set; }
        public FeeType FeeType { get; set; }
    }
}