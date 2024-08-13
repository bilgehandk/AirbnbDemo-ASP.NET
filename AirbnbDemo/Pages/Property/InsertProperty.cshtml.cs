using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.Models;
using System.IO;

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
                });

            FeeTypes = _unitOfWork.FeeType.GetAll()
                .Select(f => new SelectListItem
                {
                    Text = f.Name,
                    Value = f.FeeTypeId.ToString()
                });

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
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (Property.Id == 0)
            {
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\properties\");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    Property.MediaItems.First().UrlPath = @"\images\properties\" + fileName + extension;
                }

                _unitOfWork.Property.Add(Property);
            }
            else
            {
                var objFromDb = _unitOfWork.Property.Get(p => p.Id == Property.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\properties\");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (Property.MediaItems.First().UrlPath != null)
                    {
                        var imagePath = Path.Combine(webRootPath, objFromDb.MediaItems.First().UrlPath.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    Property.MediaItems.First().UrlPath = @"\images\properties\" + fileName + extension;
                }
                else
                {
                    Property.MediaItems.First().UrlPath = objFromDb.MediaItems.First().UrlPath;
                }

                _unitOfWork.Property.Update(Property);
            }

            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
