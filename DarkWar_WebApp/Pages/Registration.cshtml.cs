using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class RegistrationModel : PageModel
    {
        #region Properties
        private readonly AppDbContext _db;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        //[BindProperty]
        //public string Email { get; set; }
        #endregion

        public RegistrationModel(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password)/* || string.IsNullOrWhiteSpace(Email)*/)
            {
                ModelState.AddModelError(null, "Please fill all empty Fields");
                return Page();
            }

            var userExists = _db.AppUsers.Any(u => u.Username == Username);
            if (userExists)
            {
                ModelState.AddModelError(null, "Username already exist");
                return Page();
            }

            /*var emailExists = _db.AppUsers.Any(u => u.Username == Username);
            if (emailExists)
            {
                ModelState.AddModelError(null, "Email already exist");
                return Page();
            }*/

            if (Password.Length < 8)
            {
                ModelState.AddModelError(null, "Password must be longer then 8 character");
                return Page();
            }

            var hasher = new PasswordHasher<AppUser>();
            var newUser = new AppUser
            {
                Username = Username.ToLower(),
                //Email = Email,
                PasswordHash = hasher.HashPassword(null, Password)
            };

            _db.AppUsers.Add(newUser);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index"); // zurück zum Login
        }
    }
}
