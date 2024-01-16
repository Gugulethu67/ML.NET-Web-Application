using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Team21V4._5.Data;

namespace Team21V4._5.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext dbContext;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        public void OnGet()
        {
            var UserNames = dbContext.Users.ToList();
            if(UserNames.Any())
            {
                ViewData["Users"] = UserNames[0].UserName;
            }
        }
    }
}