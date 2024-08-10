using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public DateTime? EmailVerifiedAt { get; set; }
        
        public string RememberToken { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        
        public string Description { get; set; }

        [Required]
        public string ProfileImage { get; set; }

        // You might want to add additional properties or methods here
    }
}