using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
                return; //DB has been seeded
            }

			//create roles if they are not created
			//SD is a “Static Details” class we will create in Utility to hold constant strings for Roles

			_roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.ShipperRole)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();

			//Create at least one "Super Admin" or “Admin”.  Repeat the process for other users you want to seed

			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "Bilgehan",
				Email = "bilge@bilkent.edu.tr",
				PhoneNumber = "8015556919",
				Description = "Student",
                ProfileImage = "/images/CBTD.png"
			}, "Admin123*").GetAwaiter().GetResult();

			ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "rfry@weber.edu");

			_userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();

			_db.SaveChanges();
			
			// Odalar ekleniyor
            var rooms = new List<Property>
            {
                new Property
                {
                    HomeType = "Apartment",
                    RoomType = "Entire Place",
                    TotalOccupancy = 4,
                    TotalBedrooms = 2,
                    TotalBathrooms = 1,
                    Summary = "Cozy apartment in the city center.",
                    Address = "123 Main St, Cityville",
                    HasTv = true,
                    HasKitchen = true,
                    HasAirCon = true,
                    HasHeating = true,
                    HasInternet = true,
                    Price = 120,
                    PublishedAt = DateTime.Now.AddMonths(-1),
                    OwnerId = 1,
                    CreatedAt = DateTime.Now.AddMonths(-1),
                    UpdatedAt = DateTime.Now,
                    Latitude = 40.7128f,
                    Longitude = -74.0060f
                }
            };

            foreach (var room in rooms)
            {
                _db.Room.Add(room);
            }

            _db.SaveChanges();

            // Rezervasyonlar ekleniyor
            var reservations = new List<Reservations>
            {
                new Reservations
                {
                    UserId = user.Id,
                    RoomId = rooms.First().Id,
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(15),
                    Price = 120,
                    Total = 600,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            foreach (var reservation in reservations)
            {
                _db.Reservations.Add(reservation);
            }

            _db.SaveChanges();

            // Yorumlar ekleniyor
            var reviews = new List<Reviews>
            {
                new Reviews
                {
                    ReservationId = reservations.First().Id,
                    Rating = 5,
                    Comment = "Amazing stay! The host was very helpful and the apartment was spotless."
                }
            };

            foreach (var review in reviews)
            {
                _db.Reviews.Add(review);
            }

            _db.SaveChanges();

            // Medya ekleniyor
            var media = new List<Media>
            {
                new Media
                {
                    ModelId = rooms.First().Id,
                    ModelType = "Room",
                    FileName = "room1.jpg",
                    MimeType = "image/jpeg"
                }
            };

            foreach (var mediaItem in media)
            {
                _db.Media.Add(mediaItem);
            }

            _db.SaveChanges();


        }
       }
}   


