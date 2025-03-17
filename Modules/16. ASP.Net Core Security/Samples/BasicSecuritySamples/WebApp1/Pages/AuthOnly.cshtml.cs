using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp1.Pages
{
    [Authorize("LegalAge")]
    public class AuthOnlyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
