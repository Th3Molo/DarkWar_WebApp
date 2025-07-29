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

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Dummy-Daten – du kannst das später durch echte prüfen
            if (Username == "admin" && Password == "geheim123")
            {
                HttpContext.Session.SetString("LoggedIn", "true");
                return RedirectToPage("/Index");
            }

            LoginFailed = true;
            return Page();
        }
    }
}
