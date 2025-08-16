using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class RegistrationModel : PageModel
    {
        #region Properties
        public string Email { get; set; } = string.Empty;
        public string PlayerName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        #endregion

        public void OnGet()
        {

        }

        protected void Encrypt(string password)
        {

        }

        protected string Decrypt(string password)
        {
            return password;
        }
    }
}
