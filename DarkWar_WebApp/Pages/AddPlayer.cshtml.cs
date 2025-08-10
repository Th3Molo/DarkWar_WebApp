using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Linq;

namespace DarkWar_WebApp.Pages
{
    public class AddPlayerModel : PageModel
    {
        #region Properties
        private readonly ILogger<AddPlayerModel> _logger;
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
                string cpvalue = value.ToString().Replace(".", "");

                if (long.TryParse(cpvalue, CultureInfo.InvariantCulture, out long result))
                    _CP = result;
            }
        }

        [BindProperty]
        public Rank SelectedRank { get; set; }

        [BindProperty]
        public string WatchtowerLevel { get; set; }

        public IEnumerable<SelectListItem> Ranks { get; set; }
        #endregion

        #region Contrcutor
        public AddPlayerModel(AppDbContext context, ILogger<AddPlayerModel> logger)
        {
            _context = context;
            _logger = logger;
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
                _context.SaveChanges();


                CPEntry cpentry = new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Player = newPlayer,
                    PlayerID = newPlayer.ID,
                    Value = CP,
                };

                newPlayer.CP_List.Add(cpentry);

                DbTools.AddCpEntry(newPlayer.ID, newPlayer.CP_List);

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
