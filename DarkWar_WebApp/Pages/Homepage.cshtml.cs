using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class HomepageModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                // Benutzer zurück zur Login-Seite schicken und aktuelle URL als returnUrl anhängen
                return Redirect("/Index?returnUrl=" + HttpContext.Request.Path);
            }

            return Page();
        }
    }
}
