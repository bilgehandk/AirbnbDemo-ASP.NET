using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<Property> Property { get; set; }  //the physical DB table will be named Categories
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        public DbSet<AmenityType> AmenityType { get; set; }
        
        public DbSet<Amenity> Amenity { get; set; }
        
        public DbSet<Calenderavailability> CalenderAvailability { get; set; }
        
        public DbSet<Fee> Fee { get; set; }
        
        public DbSet<FeeType> FeeType { get; set; }
        
        public DbSet<Prices> Prices { get; set; }
        
        public DbSet<ReservationStatus> ReservationStatus { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        
        
    }
}
