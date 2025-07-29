using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DarkWar_WebApp.Pages
{
    public class AddPlayerModel : PageModel
    {
        [BindProperty]
        public string PlayerName { get; set; }

        [BindProperty]
        public double CP { get; set; }

        [BindProperty]
        public Rank SelectedRank { get; set; }

        [BindProperty]
        public int WatchtowerLevel { get; set; }

        public IEnumerable<SelectListItem> Ranks { get; set; }

        public void OnGet()
        {
            Ranks = Enum.GetValues(typeof(Rank))
                        .Cast<Rank>()
                        .Select(r => new SelectListItem
                        {
                            Value = r.ToString(),
                            Text = r.ToString()
                        });
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("Overview");
            }
            else
                return null;
        }
    }
}
