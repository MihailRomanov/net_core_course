using HomePageDb;
using Microsoft.AspNetCore.Mvc;
using MyHomePageWebApp.Models;

namespace MyHomePageWebApp.Controllers
{
    public class FeedbackController(HomePageContext context) : Controller
    {
        public IActionResult Index()
        {
            var items = context.FeedbackItems
                .ToList();

            return View(items);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.FeedbackItems.Add(new FeedbackItem
                {
                    PersonName = model.Name,
                    Email = model.Email,
                    Text = model.Text
                });

                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
