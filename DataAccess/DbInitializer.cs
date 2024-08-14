using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public void Initialize()
        {
            _db.Database.EnsureCreated();

            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_db.ApplicationUsers.Any())
            {
                return; // DB has been seeded
            }

            // Create roles if they are not created
            _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.PropertyOwner)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();

            // Create at least one "Super Admin" or "Admin"
            _userManager.CreateAsync( new ApplicationUser
            {
                UserName = "bilgehan@bilkent.edu",
                Email = "bilgehan@bilkent.edu",
                FirstName = "Bilgehan",
                LastName = "Demirkaya",
                PhoneNumber = "8015556919",
                StreetAddress = "123 Main Street",
                State = "OR",
                PostalCode = "84408",
                City = "Portland",
                ProfileImage = "/images/CBTD.png"
            }, "Admin123*").GetAwaiter().GetResult();
            
            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "bilge@bilkent.edu.tr");
            _userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();

            _db.SaveChanges();
            

            // Add properties
            var properties = new List<PropertyInfo>
            {
                new PropertyInfo
                {
                    PropertyName = "Summer Apartment",
                    PropertyType = "Apartment",
                    TotalOccupancy = 4,
                    TotalBedrooms = 2,
                    TotalBathrooms = 1,
                    Description = "Cozy apartment in the city center.",
                    Address = "123 Main St, Cityville",
                    City = "Cityville",
                    State = "Stateville",
                    LastUpdated = DateTime.Now,
                    OwnerId = user.Id,
                    Latitude = 40.7128f,
                    Longitude = -74.0060f
                }
            };
            _db.Property.AddRange(properties);
            _db.SaveChanges();

            // Add CalendarAvailability
            var calendarAvailabilities = new List<CalenderAvailability>
            {
                new CalenderAvailability
                {
                    PropertyId = properties.First().Id,
                    StartDate = DateTime.Now.AddDays(20),
                    EndDate = DateTime.Now.AddDays(25)
                }
            };
            _db.CalenderAvailability.AddRange(calendarAvailabilities);
            _db.SaveChanges();


            // Add Amenities
            var amenities = new List<Amenity>
            {
                new Amenity
                {
                    Id = 1,
                    Name = "Balcony"
                },new Amenity
                {
                    Id = 2,
                    Name = "Car Parking"
                }
            };
            _db.Amenity.AddRange(amenities);
            _db.SaveChanges();

            // Add Fees
            var fees = new List<Fee>
            {
                new Fee
                {
                    Id = 1,
                    FeeAmount = 100,
                    Name = "Cleaning Room"
                },new Fee
                {
                    Id = 2,
                    FeeAmount = 40,
                    Name = "Washing Clothes"
                }
            };
            _db.Fee.AddRange(fees);
            _db.SaveChanges();

            // Add ReservationStatuses
            var reservationStatuses = new List<ReservationStatus>
            {
                new ReservationStatus { Type = "Confirmed" },
                new ReservationStatus { Type = "Cancelled" }
            };
            _db.ReservationStatus.AddRange(reservationStatuses);
            _db.SaveChanges();

            // Add Reservations
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    ApplicationUserId = user.Id,
                    PropertyId = properties.First().Id,
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(15),
                    TotalPrice = 600,
                    LastUpdated = DateTime.Now,
                    ReservationStatusId = reservationStatuses.First().Id
                }
            };
            _db.Reservations.AddRange(reservations);
            _db.SaveChanges();

            // Add Reviews
            var reviews = new List<Review>
            {
                new Review
                {
                    ReservationId = reservations.First().Id,
                    Rating = 5,
                    Comment = "Amazing stay! The host was very helpful and the apartment was spotless."
                }
            };
            _db.Reviews.AddRange(reviews);
            _db.SaveChanges();
            
        }
    }
}
