using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp1.Pages
{
    public class EmptyModel : PageModel
    {
        public void OnGet()
        {
            var t = TempData["Counter"];
        }
    }
}
