using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CBTDWeb.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Infrastructure.Models.Property> objPropertyList;

        private readonly UnitOfWork _unitOfWork;

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objPropertyList = new List<Infrastructure.Models.Property>();
        }
        
        public IActionResult OnGet(DateTime? startDate, DateTime? endDate)
        {
            // Fetch properties from your data source
            var allProperties = _unitOfWork.Room.GetAll();

            if (startDate.HasValue && endDate.HasValue)
            {
                var start = startDate.Value.Date;
                var end = endDate.Value.Date;
                objPropertyList = allProperties;
                // Filter properties based on availability within the selected date range
                //objPropertyList = allProperties.Where(p => p.AvailabilityStartDate <= end && p.AvailabilityEndDate >= start).ToList();
            }
            else
            {
                objPropertyList = allProperties;
            }

            return Page();
        }

    }
}
