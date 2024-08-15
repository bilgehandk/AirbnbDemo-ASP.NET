using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTDWeb.Pages.Property
{
    public class InsertPropertyModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public PropertyInfo Property { get; set; }

        public IEnumerable<SelectListItem> Amenity { get; set; }
        public IEnumerable<SelectListItem> Fee { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;
        //helps us map the physical path to the wwwroot folder on the server amongst other things

        public InsertPropertyModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            Property = new PropertyInfo();
            Amenity = new List<SelectListItem>();
            Fee = new List<SelectListItem>();
        }

        public IActionResult OnGet(int? id)
        {
            Property = new PropertyInfo();
            Amenity = _unitOfWork.Ammenity.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
                );
            Fee = _unitOfWork.Fee.GetAll()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }
                );

            if (id == null || id == 0) //create mode
            {
                return Page();
            }

            //edit mode

            if (id != 0)  //retreive objProduct from DB
            {
                Property = _unitOfWork.Property.GetById(id);
            }

            if (Property == null) //maybe nothing returned
            {
                return NotFound();
            }
            return Page();
        }
        
        
        
        public IActionResult OnPost()
{
    string webRootPath = _webHostEnvironment.WebRootPath;
    var files = HttpContext.Request.Form.Files;

    // Define the relative path to the images folder
    var uploadsFolder = Path.Combine(webRootPath, "images", "products");

    // Ensure the uploads folder exists
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
    }

    // Check if the product is new (create)
    if (Property.Id == 0)
    {
        if (files.Count > 0)
        {
            string fileName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(files[0].FileName);
            var fullPath = Path.Combine(uploadsFolder, fileName + extension);

            // Save the uploaded image to the server
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            // Set the URL path for the image in the database
            Property.ImageUrl = $"/images/products/{fileName}{extension}";
        }
        
        
        // Add the new property to the database
        _unitOfWork.Property.Add(Property);
    }
    else
    {
        // Retrieve the existing property from the database
        var objProductFromDb = _unitOfWork.Property.Get(p => p.Id == Property.Id);

        if (files.Count > 0)
        {
            string fileName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(files[0].FileName);
            var fullPath = Path.Combine(uploadsFolder, fileName + extension);

            // Delete the existing image if a new one is uploaded
            if (objProductFromDb.ImageUrl != null)
            {
                var imagePath = Path.Combine(webRootPath, objProductFromDb.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Save the new uploaded image to the server
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            // Update the ImageUrl in the database
            Property.ImageUrl = $"/images/products/{fileName}{extension}";
        }
        else
        {
            // Preserve the existing image URL if no new image is uploaded
            Property.ImageUrl = objProductFromDb.ImageUrl;
        }

        // Update the existing property in the database
        _unitOfWork.Property.Update(Property);
    }

    // Save changes to the database
    _unitOfWork.Commit();

    // Redirect to the Products Page
    return RedirectToPage("./Index");
}



    }


}

