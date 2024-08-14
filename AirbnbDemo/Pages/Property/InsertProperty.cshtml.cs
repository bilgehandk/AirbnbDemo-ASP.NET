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

        public IEnumerable<SelectListItem> FeeType { get; set; }
        public IEnumerable<SelectListItem> AmenityType { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;
        //helps us map the physical path to the wwwroot folder on the server amongst other things

        public InsertPropertyModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            Property = new PropertyInfo();
            FeeType = new List<SelectListItem>();
            AmenityType = new List<SelectListItem>();
        }

        public IActionResult OnGet(int? id)
        {
            Property = new PropertyInfo();
            FeeType = _unitOfWork.FeeType.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.FeeTypeId.ToString()
                }
                );
            AmenityType = _unitOfWork.AmenityType.GetAll()
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
            //Form.Files[] array enctype="multipart/form-data"
            var files = HttpContext.Request.Form.Files;

            // Check if the product is new (create)
            if (Property.Id == 0)
            {
                // Check if an image was uploaded
                if (files.Count > 0)
                {
                    // Generate a unique identifier for the image name
                    string fileName = Guid.NewGuid().ToString();

                    // Define the path to store the uploaded image
                    var uploads = Path.Combine(webRootPath, @"images\products\");

                    // Get the file extension
                    var extension = Path.GetExtension(files[0].FileName);

                    // Create the full path for the uploaded image
                    var fullPath = Path.Combine(uploads, fileName + extension);

                    // Save the uploaded image to the server
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    // Set the URL path for the image in the database
                    Property.SecondImageUrl = @"\images\products\" + fileName + extension;
                    
                    // Generate a unique identifier for the image name
                    string filename2 = Guid.NewGuid().ToString();

                    // Define the path to store the uploaded image
                    var uploads2 = Path.Combine(webRootPath, @"images\products\");

                    // Get the file extension
                    var extension2 = Path.GetExtension(files[1].FileName);

                    // Create the full path for the uploaded image
                    var fullPath2 = Path.Combine(uploads2, filename2 + extension2);

                    // Save the uploaded image to the server
                    using var fileStream2 = System.IO.File.Create(fullPath2);
                    files[1].CopyTo(fileStream2);

                    // Set the URL path for the image in the database
                    Property.ImageUrl = @"\images\products\" + filename2 + extension2;
                }

                // Add the new product to the database
                _unitOfWork.Property.Add(Property);
            }
            else
            {
                // Retrieve the existing product from the database
                var objProductFromDb = _unitOfWork.Property.Get(p => p.Id == Property.Id);

                // Check if an image was uploaded
                if (files.Count > 0)
                {
                    // Generate a unique identifier for the new image name
                    string fileName = Guid.NewGuid().ToString();

                    // Define the path to store the uploaded image
                    var uploads = Path.Combine(webRootPath, @"images\products\");

                    // Get the file extension
                    var extension = Path.GetExtension(files[0].FileName);

                    // Delete the existing image associated with the product
                    if (objProductFromDb.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, Property.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Create the full path for the new uploaded image
                    var fullPath = Path.Combine(uploads, fileName + extension);

                    // Save the new uploaded image to the server
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    // Set the URL path for the new image in the database
                    Property.ImageUrl = @"\images\products\" + fileName + extension;
                }
                else
                {
                    // Update the existing product's image URL
                    objProductFromDb.ImageUrl = Property.ImageUrl;
                }

                // Update the existing product in the database
                _unitOfWork.Property.Update(Property);
            }

            // Save changes to the database
            _unitOfWork.Commit();

            // Redirect to the Products Page
            return RedirectToPage("./Index");
        }


    }


}

