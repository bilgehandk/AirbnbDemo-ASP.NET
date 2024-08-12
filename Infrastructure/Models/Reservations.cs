using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Reservations
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } // Update to string for IdentityUser
        public ApplicationUser User { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Property Property { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } 
        
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}