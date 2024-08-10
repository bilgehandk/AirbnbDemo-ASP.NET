using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        [BindProperty]  //synchronizes form fields with values in code behind
        public Manufacturer objManufacturer { get; set; }

        public DeleteModel(ApplicationDbContext db)  //dependency injection
        {
            _db = db;
            objManufacturer = new Manufacturer();
        }

        public IActionResult OnGet(int? id)

        {
            objManufacturer = new Manufacturer();

            //am I in edit mode?
            if (id != 0)
            {
                objManufacturer = _db.Manufacturers.Find(id);
            }

            if (objManufacturer == null)  //nothing found in DB
            {
                return NotFound();   //built in page
            }

            //assuming I'm in create mode
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Manufacturers.Remove(objManufacturer);  //Removes from memory
            TempData["success"] = "Manufacturer Deleted Successfully";
            _db.SaveChanges();   //saves to DB

            return RedirectToPage("./Index");
        }

    }
}
