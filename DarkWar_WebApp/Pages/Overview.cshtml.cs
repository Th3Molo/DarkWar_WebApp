using DarkWar_WebApp;
using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DarkWar_WebApp.Pages 
{
    public class OverviewModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Player> PlayerList { get; set; } = new();

        public OverviewModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var orderedtable = _context.Players.OrderByDescending(p => p.Rank)
                                                   .ThenByDescending(p => p.CP)
                                                   .ThenBy(p => p.PlayerName);

                PlayerList = await orderedtable.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Laden der Daten: " + ex.Message);
                // Option: eine Fehlermeldung an ViewData senden:
                ViewData["Fehler"] = "Die Spielerdaten konnten nicht geladen werden.";
            }
        }
    }
}