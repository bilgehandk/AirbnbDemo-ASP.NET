using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _db.Database.EnsureCreated();

            // Migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                // Handle the exception appropriately
            }

            if (_db.ApplicationUsers.Any())
            {
                return; // DB has been seeded
            }

            // Create roles if they are not created
            _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.ShipperRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();

            // Create at least one "Super Admin" or "Admin"
            var adminUser = new ApplicationUser
            {
                Name = "Bilgehan",
                Email = "bilge@bilkent.edu.tr",
                PhoneNumber = "8015556919",
                Description = "Student",
                ProfileImage = "/images/CBTD.png",
                EmailVerifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            
            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.AdminRole).GetAwaiter().GetResult();

            _db.SaveChanges();

            // Add FeeTypes
            var feeTypes = new List<FeeType>
            {
                new FeeType { Name = "Cleaning Fee", Description = "Fee for cleaning the property after checkout." },
                new FeeType { Name = "Service Fee", Description = "Service fee for handling the reservation." }
            };
            _db.FeeType.AddRange(feeTypes);
            _db.SaveChanges();

            // Add properties
            var properties = new List<Property>
            {
                new Property
                {
                    PropertyType = "Apartment",
                    TotalOccupancy = 4,
                    TotalBedrooms = 2,
                    TotalBathrooms = 1,
                    Description = "Cozy apartment in the city center.",
                    Address = "123 Main St, Cityville",
                    City = "Cityville",
                    State = "Stateville",
                    LastUpdated = DateTime.Now,
                    OwnerId = adminUser.Id,
                    Latitude = 40.7128f,
                    Longitude = -74.0060f
                }
            };
            _db.Property.AddRange(properties);
            _db.SaveChanges();

            // Add CalendarAvailability
            var calendarAvailabilities = new List<CalenderAvaliability>
            {
                new CalenderAvaliability
                {
                    PropertyId = properties.First().Id,
                    StartDate = DateTime.Now.AddDays(20),
                    EndDate = DateTime.Now.AddDays(25)
                }
            };
            _db.CalenderAvaliability.AddRange(calendarAvailabilities);
            _db.SaveChanges();

            // Add AmenityTypes
            var amenityTypes = new List<AmenityType>
            {
                new AmenityType { Name = "WiFi" },
                new AmenityType { Name = "Air Conditioning" }
            };
            _db.AmenityType.AddRange(amenityTypes);
            _db.SaveChanges();

            // Add Amenities
            var amenities = new List<Ammenity>
            {
                new Ammenity
                {
                    PropertyId = properties.First().Id,
                    AmenityTypeId = amenityTypes.First().AmneityTypeId
                }
            };
            _db.Amenity.AddRange(amenities);
            _db.SaveChanges();

            // Add Prices
            var prices = new List<Prices>
            {
                new Prices
                {
                    PropertyID = properties.First().Id,
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(10),
                    PriceAmount = 150
                }
            };
            _db.Prices.AddRange(prices);
            _db.SaveChanges();

            // Add Fees
            var fees = new List<Fee>
            {
                new Fee
                {
                    PropertyId = properties.First().Id,
                    FeeAmount = 50,
                    FeeTypeId = feeTypes.First().FeeTypeId
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
                    UserId = adminUser.Id,
                    PropertyId = properties.First().Id,
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(15),
                    TotalPrice = 600,
                    LastUpdated = DateTime.Now,
                    ReservationStatusId = reservationStatuses.First().ReservationStatusId
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

            // Add Media
            var mediaItems = new List<Media>
            {
                new Media
                {
                    MediaId = 1,
                    UrlPath = "room1.jpg",
                    IsMainImage = true
                }
            };
            _db.Media.AddRange(mediaItems);
            _db.SaveChanges();
        }
    }
}
