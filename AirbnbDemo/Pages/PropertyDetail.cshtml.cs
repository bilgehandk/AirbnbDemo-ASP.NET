using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CBTDWeb.Pages
{
    public class PropertyDetailModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        public PropertyInfo objProperty { get; set; }
        [BindProperty]
        public int hostCount { get; set; }
        public ApplicationUser hostUser { get; set; }
        public CalenderAvailability calenderAvailability { get; set; }
        public Reservation objCart { get; set; }

        public PropertyDetailModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objProperty = new PropertyInfo();
            hostUser = new ApplicationUser();
            calenderAvailability = new CalenderAvailability();
            objCart = new Reservation();
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            //TempData["UserLoggedIn"] = claim;

            objProperty = _unitOfWork.Property.Get(p => p.Id == id, includes: "Amenity,Fee");
            if (objProperty == null)
            {
                return NotFound();
            }

            hostUser = _unitOfWork.ApplicationUser.Get(u => u.Id == objProperty.OwnerId) ?? new ApplicationUser();
            calenderAvailability = _unitOfWork.CalenderAvaliablity.Get(u => u.PropertyId == objProperty.Id) ?? new CalenderAvailability();

            return Page();
        }

        public IActionResult OnPost(PropertyInfo property, DateTime startDate, DateTime endDate)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fee = property.Fee?.FeeAmount ?? 0;
            var price = property.Prices;

            var existingReserve = _unitOfWork.Reservations.Get(u => u.ApplicationUserId == userId && u.PropertyId == property.Id);

            if (existingReserve == null)
            {
                var reservation = new Reservation
                {
                    ApplicationUserId = userId,
                    PropertyId = property.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalPrice = (int)getTotalPrice(price, startDate, endDate, fee),
                    LastUpdated = DateTime.UtcNow,
                    ReservationStatus = new ReservationStatus { Type = "Request" }
                };
                _unitOfWork.Reservations.Add(reservation);
            }
            else
            {
                existingReserve.StartDate = startDate;
                existingReserve.EndDate = endDate;
                existingReserve.TotalPrice = (int)getTotalPrice(price, startDate, endDate, fee);
                existingReserve.LastUpdated = DateTime.UtcNow;
                _unitOfWork.Reservations.Update(existingReserve);
            }

            _unitOfWork.Commit();
            return RedirectToPage("Reservation");
        }

        private double getTotalPrice(double price, DateTime startDate, DateTime endDate, float fee)
        {
            var duration = (endDate - startDate).TotalDays;
            var totalPrice = duration * price + fee;
            return totalPrice;
        }
    }
}
