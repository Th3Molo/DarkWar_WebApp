using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        #region Properties
        private readonly AppDbContext _db;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }

        public bool LoginFailed { get; set; }
        #endregion

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            // Nur weiterleiten, wenn wir nicht auf der Login-Seite sind
            if (HttpContext.Session.GetString("IsLoggedIn") != "true" && !HttpContext.Request.Path.Equals("/Index", StringComparison.OrdinalIgnoreCase))
            {
                return Redirect("/Index?returnUrl=" + HttpContext.Request.Path);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _db.AppUsers.SingleOrDefault(u => u.Username == Username.ToLower());
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                ModelState.AddModelError("", "Go to registration or try another User");
                return Page();
            }

            var hasher = new PasswordHasher<AppUser>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, Password);

            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", user.Username);

                if (!string.IsNullOrEmpty(ReturnUrl))
                    return LocalRedirect(ReturnUrl);

                return LocalRedirect("/Homepage");
            }

            ModelState.AddModelError("", "Password incorrect");
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear(); // Oder nur: Remove("IsLoggedIn")
            return RedirectToPage("/Index");
        }

    }
}
