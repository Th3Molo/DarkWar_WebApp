using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq;

namespace DarkWar_WebApp.Pages
{
    public class AddPlayerModel : PageModel
    {
        #region Properties
        private readonly AppDbContext _context;

        [BindProperty]
        public string PlayerName { get; set; }

        private long _CP = 0;

        [BindProperty]
        public long CP
        {
            get => _CP;
            set 
            { 
                if (long.TryParse(value.ToString(), out long result))
                    value = result;

                _CP = value;
            }
        }


        [BindProperty]
        public Rank SelectedRank { get; set; }

        [BindProperty]
        public int WatchtowerLevel { get; set; }

        public IEnumerable<SelectListItem> Ranks { get; set; }
        #endregion

        #region Contrcutor
        public AddPlayerModel(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
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
            if (!ModelState.IsValid)
                return Page();

            var newPlayer = new Player
            {
                PlayerName = PlayerName,
                CP = CP,
                Rank = SelectedRank,
                WatchtowerLevel = WatchtowerLevel
            };

            if (!_context.Players.Any(p => p.PlayerName == newPlayer.PlayerName))
            {
                _context.Players.Add(newPlayer);
                _context.SaveChanges(); // oder await ... wenn async                

                TempData["SuccessMessage"] = "Player added to Database";
                return RedirectToPage("Overview");
            }
            else
            {
                TempData["DoublePlayer"] = "Player already added to Database";
                return RedirectToPage("AddPlayer");
            }
        }
        #endregion
    }
}
