using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Controllers
{
    public class HomeController : Controller
    {
        [TempData]
        public int Counter { get; set; }

        public IActionResult Index()
        {
            Counter++;
            return View(HttpContext);
        }
    }
}
