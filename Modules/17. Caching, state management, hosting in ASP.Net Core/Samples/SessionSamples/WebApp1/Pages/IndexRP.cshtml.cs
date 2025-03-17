using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp1.Pages
{
    public class IndexModel : PageModel
    {
        [TempData]
        public int Counter { get; set; }

        public void OnGet()
        {
            Counter++;
        }
    }
}
