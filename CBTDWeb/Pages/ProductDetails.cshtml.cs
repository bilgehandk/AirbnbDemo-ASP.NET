using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages
{
    public class ProductDetailsModel : PageModel
    {
		private readonly UnitOfWork _unitOfWork;

		public Product objProduct;

		[BindProperty]
		public int txtCount { get; set; }

		public ProductDetailsModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			objProduct = new Product();
		}

		// In the HTML page the asp-route-productId is why we get the int and the name
		public IActionResult OnGet(int? id)
		{
			objProduct = _unitOfWork.Product.Get(p => p.Id == id, includes: "Category,Manufacturer");
			return Page();
		}

	}
}

// SELECT * FROM PRODUCTS P
// JOIN CATEGORIES C ON C.PRODUCTID = P.PRODUCTID
// JOIN MANUFACTURERS M ON M.PRODUCTID = P.PRODUCTID
// WHERE P.ID = @ID
