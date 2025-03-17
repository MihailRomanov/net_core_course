using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp1.Pages.Admin
{
    [Authorize("ManagerAndLegalAge")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
