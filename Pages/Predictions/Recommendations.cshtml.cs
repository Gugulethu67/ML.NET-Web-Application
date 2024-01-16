using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Team21V4._5.Model;

namespace Team21V4._5.Pages.Predictions
{
    [Authorize("LoggedInPolicy")]
    public class RecommendationsModel : PageModel
    {
        private readonly Team21V4._5.Data.ApplicationDbContext _context;

        public RecommendationsModel(Team21V4._5.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? search { get; set; }
        public List<Product> Product { get; private set; }

        public async Task OnGetAsync()
        {
            var product = from p in _context.Products select p;
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.ProductName.Contains(search));
            }
            Product = await product.ToListAsync();
        }
    }
}
