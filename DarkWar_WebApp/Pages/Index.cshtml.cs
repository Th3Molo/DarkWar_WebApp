using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public bool LoginFailed { get; set; }

        public IActionResult OnGet()
        {
            // Nur weiterleiten, wenn wir nicht auf der Login-Seite sind
            if (HttpContext.Session.GetString("IsLoggedIn") != "true" && !HttpContext.Request.Path.Equals("/Index", StringComparison.OrdinalIgnoreCase))
            {
                return Redirect("/Index?returnUrl=" + HttpContext.Request.Path);
            }

            return Page();
        }

        public IActionResult OnPost(string username, string password, string returnUrl = null)
        {
            if (username == "admin" && password == "geheim1234")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return LocalRedirect("/Homepage");
            }

            ModelState.AddModelError("", "Login fehlgeschlagen");
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear(); // Oder nur: Remove("IsLoggedIn")
            return RedirectToPage("/Index");
        }

    }
}
