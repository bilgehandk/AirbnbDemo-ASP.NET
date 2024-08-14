using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace CBTDWeb.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<PropertyInfo> objPropertyList { get; set; }

        private readonly UnitOfWork _unitOfWork;

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPropertyList = new List<PropertyInfo>();
        }

        public IActionResult OnGet(DateTime? startDate, DateTime? endDate)
        {
            // Fetch properties, availability, and reservations data from your data source
            var allProperties = _unitOfWork.Property.GetAll().ToList();
            var calendarAvailabilities = _unitOfWork.CalenderAvaliablity.GetAll().ToList();
            var reservations = _unitOfWork.Reservations.GetAll().ToList();

            // Handle date filtering
            if (startDate.HasValue && endDate.HasValue)
            {
                var start = startDate.Value.Date;
                var end = endDate.Value.Date;

                // Step 1: Filter available properties based on calendar availability
                var availablePropertyIds = calendarAvailabilities
                    .Where(a => a.StartDate <= end && a.EndDate >= start)
                    .Select(a => a.PropertyId)
                    .Distinct()
                    .ToList();

                // Step 2: Exclude properties that have reservations overlapping with the selected date range
                var reservedPropertyIds = reservations
                    .Where(r => r.StartDate <= end && r.EndDate >= start)
                    .Select(r => r.PropertyId)
                    .Distinct()
                    .ToList();

                var availablePropertyIdsAfterReservationCheck = availablePropertyIds
                    .Except(reservedPropertyIds)
                    .ToList();

                // Step 3: Filter properties based on the refined list of available property IDs
                objPropertyList = allProperties
                    .Where(p => availablePropertyIdsAfterReservationCheck.Contains(p.Id))
                    .ToList();
            }
            else
            {
                // If no date filter is applied, show all properties
                objPropertyList = allProperties;
            }

            return Page();
        }
    }
}
