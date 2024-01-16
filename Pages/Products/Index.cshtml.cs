using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Team21V4._5.Data;
using Team21V4._5.Model;

namespace Team21V4._5.Pages.Products
{
    [Authorize("LoggedInPolicy")]
    public class IndexModel : PageModel
    {
        private readonly Team21V4._5.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IndexModel(Team21V4._5.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [BindProperty]
        public IFormFile UploadedFile { get; set; }




        public IList<Product> Product { get;set; } = default!;
        
        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products.ToListAsync();
            }
            ViewData["productID"] = Product[0].ProductID;
            TempData["productID"] = Product[0].ProductID;
        }
        public IActionResult OnPost()
        {
            if (UploadedFile != null && UploadedFile.Length > 0)
            {
                try
                {
                    using (var streamReader = new StreamReader(UploadedFile.OpenReadStream()))
                    {
                        string line;
                        bool skipHeader = true; // Skip the header line
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (skipHeader)
                            {
                                skipHeader = false;
                                continue;
                            }

                            string[] columns = line.Split(';');

                            var product = new Product
                            {
                                ProductID = int.Parse(columns[0]),
                                ProductName = columns[1],
                                DateAdded = DateTime.Parse(columns[2]),
                                StockOnHand = int.Parse(columns[3]),
                                //DeliveredQuantity = int.Parse(columns[4]),
                                Department = columns[4],
                                //WasteQuantity = int.Parse(columns[6]),
                                //BoughtStock = int.Parse(columns[7]),
                                Temperature = float.Parse(columns[6].Substring(0, 2)),
                                
                                //Precipitation = float.Parse(columns[9].Substring(0, 1))
                            };

                            _context.Products.Add(product);
                        }

                        _context.SaveChanges();
                    }

                    // File uploaded and data inserted into the database.
                    return RedirectToPage("./Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("UploadedFile", $"Error: {ex.Message}");
                }
            }

            // Handle the case where no file was uploaded or there was an error.
            ModelState.AddModelError("UploadedFile", "Please select a valid file to upload.");
            return Page();
        }

    }
}
