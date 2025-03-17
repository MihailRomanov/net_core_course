using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApp2.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty, DisplayName("Логин"), Required]
        public string Login { get; set; }

        [BindProperty, DisplayName("Пароль"), Required]
        public string Password { get; set; }

        public void OnGet() {}

        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var claims = new Claim[]
                    {
                        new(ClaimTypes.Name, Login),
                        new(ClaimTypes.Role, "Manager"),
                    };

                var principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Form"));

                await HttpContext.SignInAsync(principal);

                return Redirect(returnUrl ?? "/");
            }

            return Page();
        }

    }
}
