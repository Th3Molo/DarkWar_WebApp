using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DarkWar_WebApp.Pages
{
    public class PlayerDetailsModel : PageModel
    {
        private readonly AppDbContext _db;

        public PlayerDetailsModel(AppDbContext db)
        {
            _db = db;
        }

        public Player Player { get; set; }

        [BindProperty]
        public string PlayerName { get; set; }

        [BindProperty]
        public long CP { get; set; }

        [BindProperty]
        public Rank SelectedRank { get; set; }

        [BindProperty]
        public string WatchtowerLevel { get; set; }

        public List<CPEntry> CP_List { get; set; }

        public IEnumerable<SelectListItem> Ranks { get; set; }

        public IActionResult OnGet(string playerName)
        {
            var player = _db.Players.FirstOrDefault(p => p.PlayerName == playerName);

            if (player == null)
                return NotFound();

            player.CP_List = DbTools.LoadCpEntry(player.ID);

            // Felder mit den Werten des Spielers füllen
            PlayerName = player.PlayerName;
            CP = player.CP;
            SelectedRank = player.Rank;
            WatchtowerLevel = player.WatchtowerLevel;
            CP_List = player.CP_List;

            // Dropdown für Ranks
            Ranks = Enum.GetValues(typeof(Rank))
                        .Cast<Rank>()
                        .Select(r => new SelectListItem
                        {
                            Value = r.ToString(),
                            Text = r.ToString()
                        });

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var player = _db.Players.FirstOrDefault(p => p.PlayerName == PlayerName);
            if (player == null)
                return NotFound();

            // Werte aktualisieren
            player.CP = CP;
            player.Rank = SelectedRank;
            player.WatchtowerLevel = WatchtowerLevel;
            player.AddCpToList(CP, DateOnly.FromDateTime(DateTime.Now));            

            _db.SaveChanges();

            return RedirectToPage("/Overview");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var member = await _db.Players.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _db.Players.Remove(member);
            await _db.SaveChangesAsync();

            return RedirectToPage(); // Oder woanders hin, z.B. Liste aktualisieren
        }
    }
}
