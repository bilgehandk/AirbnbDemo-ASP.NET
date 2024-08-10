using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _UnitOfWork;
        [BindProperty]  //synchronizes form fields with values in code behind
        public Category objCategory { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)  //dependency injection
        {
            _UnitOfWork = unitOfWork;
            objCategory = new Category();
        }

        public IActionResult OnGet(int? id)

        {
            objCategory = new Category();

            //am I in edit mode?
            if (id != 0)
            {
                objCategory = _UnitOfWork.Category.GetById(id);
            }

            if (objCategory == null)  //nothing found in DB
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
            _UnitOfWork.Category.Delete(objCategory);  //Removes from memory
            TempData["success"] = "Category Deleted Successfully";
            _UnitOfWork.Commit();   //saves to DB

            return RedirectToPage("./Index");
        }


    }
}
