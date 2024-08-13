using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CBTDWeb.Pages.Property
{
    public class InsertPropertyModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InsertPropertyModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public PropertyInfo Property { get; set; }

        [BindProperty]
        public IList<string> SelectedAmenityTypes { get; set; } = new List<string>();

        [BindProperty]
        public IList<string> SelectedFeeTypes { get; set; } = new List<string>();

        public IEnumerable<SelectListItem> AmenityTypes { get; set; }
        public IEnumerable<SelectListItem> FeeTypes { get; set; }

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

                // Populate selected values based on existing data
                SelectedAmenityTypes = Property.Amenities?.Select(a => a.AmenityTypeId.ToString()).ToList() ?? new List<string>();
                SelectedFeeTypes = Property.Prices?.Select(p => p.Fees.FirstOrDefault().FeeTypeId.ToString()).Distinct().ToList() ?? new List<string>();

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

                    Property.MediaItems = new List<Media>
                    {
                        new Media { UrlPath = @"\images\properties\" + fileName + extension }
                    };
                }

                _unitOfWork.Property.Add(Property);
                _unitOfWork.Commit();
                Property.Id = _unitOfWork.Property.GetAll().Last().Id; // Get the ID of the newly added property
            }
            else
            {
                var objFromDb = _unitOfWork.Property.Get(p => p.Id == Property.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\properties\");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (Property.MediaItems != null && Property.MediaItems.Any())
                    {
                        var existingMediaPath = Path.Combine(webRootPath, objFromDb.MediaItems.First().UrlPath.TrimStart('\\'));
                        if (System.IO.File.Exists(existingMediaPath))
                        {
                            System.IO.File.Delete(existingMediaPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    
                    if (Property.MediaItems == null)
                    {
                        Property.MediaItems = new List<Media>();
                    }

                    Property.MediaItems.Add(new Media { UrlPath = @"\images\properties\" + fileName + extension });
                }

                _unitOfWork.Property.Update(Property);
                _unitOfWork.Commit();
            }

            // Update amenities
            if (Property.Id > 0)
            {
                var existingAmenities = _unitOfWork.Amenity.GetAll().Where(a => a.PropertyId == Property.Id).ToList();
                _unitOfWork.Amenity.Delete(existingAmenities);

                foreach (var amenityTypeId in SelectedAmenityTypes)
                {
                    var amenityTypeIdInt = int.Parse(amenityTypeId);
                    _unitOfWork.Amenity.Add(new Amenity
                    {
                        PropertyId = Property.Id,
                        AmenityTypeId = amenityTypeIdInt
                    });
                }
                _unitOfWork.Commit();
            }

            // Update fees
            if (Property.Id > 0)
            {
                var existingFees = _unitOfWork.Fee.GetAll().Where(f => f.PropertyId == Property.Id).ToList();
                _unitOfWork.Fee.Delete(existingFees);

                foreach (var feeTypeId in SelectedFeeTypes)
                {
                    var feeTypeIdInt = int.Parse(feeTypeId);
                    _unitOfWork.Fee.Add(new Fee
                    {
                        PropertyId = Property.Id,
                        FeeAmount = 50, // Default fee amount or set as per your requirement
                        FeeTypeId = feeTypeIdInt
                    });
                }
                _unitOfWork.Commit();
            }

            return RedirectToPage("./Index");
        }
    }
}
