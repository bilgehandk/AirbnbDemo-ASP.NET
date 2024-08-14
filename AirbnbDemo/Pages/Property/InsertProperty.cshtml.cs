using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty] public PropertyInfo Property { get; set; }
        [BindProperty] public CalenderAvailability CalenderAvailability { get; set; }

        public IEnumerable<SelectListItem> Amenity { get; set; }
        public IEnumerable<SelectListItem> Fee { get; set; }

        public InsertPropertyModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            Property = new PropertyInfo();
            CalenderAvailability = new CalenderAvailability();
        }

        public IActionResult OnGet(int? id)
        {
            Amenity = _unitOfWork.Ammenity.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

            Fee = _unitOfWork.Fee.GetAll()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });

            if (id == null || id == 0)
            {
                Property = new PropertyInfo();
                return Page();
            }

            Property = _unitOfWork.Property.GetById(id.Value);
            if (Property == null)
            {
                return NotFound();
            }

            // Populate CalenderAvailability if it exists
            CalenderAvailability = _unitOfWork.CalenderAvaliablity.Get(ca => ca.PropertyId == Property.Id);
            if (CalenderAvailability == null)
            {
                CalenderAvailability = new CalenderAvailability { PropertyId = Property.Id };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            Property.OwnerId = userId;

            var uploadsFolder = Path.Combine(webRootPath, "images", "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (Property.Id == 0)
            {
                // Handle file uploads
                Property.ImageUrl = await SaveFileAsync(files.ElementAtOrDefault(0), uploadsFolder);
                Property.SecondImageUrl = await SaveFileAsync(files.ElementAtOrDefault(1), uploadsFolder);

                _unitOfWork.Property.Add(Property);
                await _unitOfWork.CommitAsync();

                // Add Calendar Availability
                CalenderAvailability.PropertyId = Property.Id;
                _unitOfWork.CalenderAvaliablity.Add(CalenderAvailability);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                var objProductFromDb = _unitOfWork.Property.Get(p => p.Id == Property.Id);

                // Handle file uploads
                Property.ImageUrl = await SaveFileAsync(files.ElementAtOrDefault(0), uploadsFolder, objProductFromDb.ImageUrl);
                Property.SecondImageUrl = await SaveFileAsync(files.ElementAtOrDefault(1), uploadsFolder, objProductFromDb.SecondImageUrl);

                _unitOfWork.Property.Update(Property);
                await _unitOfWork.CommitAsync();

                // Update Calendar Availability
                var calenderAvailability = _unitOfWork.CalenderAvaliablity.Get(ca => ca.PropertyId == Property.Id);
                if (calenderAvailability != null)
                {
                    calenderAvailability.StartDate = CalenderAvailability.StartDate;
                    calenderAvailability.EndDate = CalenderAvailability.EndDate;
                    _unitOfWork.CalenderAvaliablity.Update(calenderAvailability);
                    await _unitOfWork.CommitAsync();
                }
            }

            return RedirectToPage("../Index");
        }

        private async Task<string> SaveFileAsync(IFormFile file, string uploadsFolder, string existingFilePath = null)
        {
            if (file == null || file.Length == 0)
                return existingFilePath;

            // Delete existing file
            if (!string.IsNullOrEmpty(existingFilePath))
            {
                var existingFilePathFull = Path.Combine(_webHostEnvironment.WebRootPath, existingFilePath.TrimStart('/'));
                if (System.IO.File.Exists(existingFilePathFull))
                {
                    System.IO.File.Delete(existingFilePathFull);
                }
            }

            var fileName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(uploadsFolder, fileName + extension);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"../images/products/{fileName}{extension}";
        }
    }
}
