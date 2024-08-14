using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages;

public class CalendarPrice : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    
    [BindProperty]  //synchronizes form fields with values in code behind
    public Prices objPrice { get; set; }

    public CalendarPrice(UnitOfWork unitOfWork) // dependency injection
    {
        _unitOfWork = unitOfWork;
        objPrice = new Prices();
    }
    
    public void OnGet(int Propertyid)
    {
        objPrice.PropertyId = Propertyid;
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }


        _unitOfWork.Prices.Add(objPrice);
        TempData["success"] = "Category added Successfully";


        _unitOfWork.Commit();

        return RedirectToPage("./Index");
    }
}