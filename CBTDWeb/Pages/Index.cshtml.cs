using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Product>? objProductList;
        public IEnumerable<Category>? objCategoryList { get; set; }
        private readonly UnitOfWork _unitOfWork;

        public IndexModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            objProductList = new List<Product>();
            objCategoryList = new List<Category>();
        }

        public IActionResult OnGet()
        {
            objCategoryList = _unitOfWork.Category.GetAll(null, c => c.DisplayOrder, null);
            objProductList = _unitOfWork.Product.GetAll(null, includes: "Category,Manufacturer");
            return Page();
        }

    }
}
    