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

        public DbSet<Room> Room { get; set; }  //the physical DB table will be named Categories
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        
    }
}
