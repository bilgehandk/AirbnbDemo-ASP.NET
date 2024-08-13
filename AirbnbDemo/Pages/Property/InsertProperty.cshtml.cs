using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.Models;
using System.IO;
using System.Linq;

namespace CBTDWeb.Pages.Property
{
    public class InsertPropertyModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public PropertyInfo Property { get; set; }

        public IEnumerable<SelectListItem> AmenityTypes { get; set; }
        public IEnumerable<SelectListItem> FeeTypes { get; set; }

        public InsertPropertyModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            AmenityTypes = _unitOfWork.AmenityType.GetAll()
                .Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToList(); // Ensure it's a list to avoid potential issues with lazy loading

            FeeTypes = _unitOfWork.FeeType.GetAll()
                .Select(f => new SelectListItem
                {
                    Text = f.Name,
                    Value = f.FeeTypeId.ToString()
                }).ToList(); // Same as above

            if (id == null || id == 0)
            {
                // Create mode
                Property = new PropertyInfo();
                return Page();
            }
            else
            {
                // Edit mode
                Property = _unitOfWork.Property.Get(p => p.Id == id);

                if (Property == null)
                {
                    return NotFound();
                }

                return Page();
            }
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\properties\");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                // Ensure the MediaItems list is initialized and has at least one item
                if (Property.MediaItems == null || Property.MediaItems.Count == 0)
                {
                    Property.MediaItems = new List<Media>
                    {
                        new Media { UrlPath = @"\images\properties\" + fileName + extension }
                    };
                }
                else
                {
                    Property.MediaItems.First().UrlPath = @"\images\properties\" + fileName + extension;
                }
            }

            if (Property.Id == 0)
            {
                _unitOfWork.Property.Add(Property);
            }
            else
            {
                var objFromDb = _unitOfWork.Property.Get(p => p.Id == Property.Id);
                if (objFromDb != null)
                {
                    _unitOfWork.Property.Update(Property);
                }
            }

            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
