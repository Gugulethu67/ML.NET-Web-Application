using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Team21V4._5.Pages.DashBoard
{
    [Authorize("LoggedInPolicy")]
    public class TimeframeSelectionModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
