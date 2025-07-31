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

        public IActionResult OnPost(string username, string password)
        {
            if (username == "admin" && password == "geheim1234")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToPage("/AddPlayer");
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
