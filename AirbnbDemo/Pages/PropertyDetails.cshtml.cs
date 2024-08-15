using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CBTDWeb.Pages
{
    public class PropertyDetailsModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        public PropertyInfo objProperty { get; set; }
        [BindProperty]
        public int hostCount { get; set; }
        public ApplicationUser hostUser { get; set; }
        public CalenderAvailability calenderAvailability { get; set; }
        public Reservation objCart = new();

        public PropertyDetailsModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objProperty = new PropertyInfo();
            hostUser = new ApplicationUser();
            calenderAvailability = new CalenderAvailability();
        }

        public IActionResult OnGet(int? id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity; // get the user object that is returned when logged >> might be a list of users
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier); //grab the logged in user, normally at idx 0
            TempData["UserLoggedIn"] = claim;


            //FOR OUTER JOIN , CATEGORY.MANUFACTUER, STUDENT = category left join manufacturer and join student
            objProperty = _unitOfWork.Property.Get(p => p.Id == id, includes: "Amenity,Fee");
            hostUser = _unitOfWork.ApplicationUser.Get(u => u.Id == objProperty.OwnerId);
            calenderAvailability = _unitOfWork.CalenderAvaliablity.Get(u => u.PropertyId == objProperty.Id);
            // the p here stored the result of the sql querry
            // SELECT * FROM PRODUCTS P
            // JOIN CATEGORIES C ON C.PRODUCTID = P.PRODUCTID
            // JOIN MANUFACTURERS M ON M.PRODUCTID = P.PRODUCTID
            // WHERE P.ID = @ID
            return Page();
        }

        //    public IActionResult OnPost(PropertyInfo property, DateTime startDate, DateTime endDate)
        //    {
        //        //check if user is logged in
        //        var claimsIdentity = User.Identity as ClaimsIdentity;
        //        var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var fee = property.Fee.FeeAmount;
        //        var price = property.Prices;


        //        //check if the reservation is existed or not
        //        var existingReserve = _unitOfWork.Reservations.Get(
        //            u => u.ApplicationUserId == userId && u.PropertyId == objProperty.Id);
        //        if (existingReserve == null)
        //        {
        //            var reservation = new Reservation
        //            {
        //                ApplicationUserId = userId,
        //                PropertyId = objProperty.Id,
        //                StartDate = startDate,
        //                EndDate = endDate,
        //                // totalHost = hostCount;
        //                TotalPrice = (int)getTotalPrice(price, startDate, endDate, fee),
        //                LastUpdated = DateTime.UtcNow,
        //                ReservationStatus = new()
        //            };
        //            reservation.ReservationStatus.Type = "Request";
        //        }
        //        else
        //        {
        //            _unitOfWork.Reservations.updateReservation(existingReserve, hostCount);
        //            _unitOfWork.Reservations.Update(existingReserve);
        //        }
        //        _unitOfWork.Commit();
        //        return RedirectToPage("Reservation");
        //    }

        //    private double getTotalPrice(double price, DateTime startDate, DateTime endDate, float fee)
        //    {
        //        var duration = (endDate - startDate).TotalDays;
        //        var totalPrice = duration * price + (double)fee;
        //        return totalPrice;
        //    }
    }
}
