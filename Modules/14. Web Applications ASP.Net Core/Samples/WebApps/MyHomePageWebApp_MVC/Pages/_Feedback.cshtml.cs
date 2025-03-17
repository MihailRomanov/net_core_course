using HomePageDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace MyHomePageWebApp.Pages
{
    [IgnoreAntiforgeryToken]
    public class FeedbackModel(HomePageContext context) : PageModel
    {
        public IEnumerable<FeedbackItem> FeedbackItems { get; set; }

        [BindProperty, DisplayName("���")]
        public string Name { get; set; }

        [BindProperty, DisplayName("�����")]
        public string Email { get; set; }

        [BindProperty, DisplayName("�����")]
        public string Text { get; set; }

        public void OnGet()
        {
            FeedbackItems = context
                .FeedbackItems
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                context.FeedbackItems.Add(new FeedbackItem
                {
                    Email = Email,
                    PersonName = Name,
                    Text = Text,
                });

                context.SaveChanges();
            }

            return RedirectToPage();
        }
    }

}
