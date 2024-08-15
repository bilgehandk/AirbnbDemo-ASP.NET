using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages;

public class CalendarPrice : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    
    [BindProperty]  //synchronizes form fields with values in code behind
    public CalenderAvailability objAvailable { get; set; }

    public CalendarPrice(UnitOfWork unitOfWork) // dependency injection
    {
        _unitOfWork = unitOfWork;
        objAvailable = new CalenderAvailability();
    }
    
    public void OnGet(int Propertyid)
    {
        objAvailable.PropertyId = Propertyid;
        objAvailable.PropertyId = 1;
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }


        _unitOfWork.CalenderAvaliablity.Add(objAvailable);
        TempData["success"] = "Category added Successfully";


        _unitOfWork.Commit();

        return RedirectToPage("./Index");
    }
}