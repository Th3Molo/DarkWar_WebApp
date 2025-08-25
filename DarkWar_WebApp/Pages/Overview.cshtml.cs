using DarkWar_WebApp;
using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        public void OnGet()
        {
            try
            {
                var orderedtable = _context.Players.OrderByDescending(p => p.Rank)
                                                   .ThenByDescending(p => p.CP)
                                                   .ThenBy(p => p.PlayerName);

                PlayerList = orderedtable.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Laden der Daten: " + ex.Message);
                // Option: eine Fehlermeldung an ViewData senden:
                ViewData["Fehler"] = "Die Spielerdaten konnten nicht geladen werden.";
            }
        }

        public async Task<IActionResult> OnPostAsync(IFormFile csvFile)
        {
            List<Player> playerlist = new List<Player>();

            if (csvFile == null || csvFile.Length == 0)
            {
                ModelState.Remove("csvFile"); // entfernt die Auto-Meldung
                ModelState.AddModelError(string.Empty, "Please Upload CSV Data");

                var orderedtable = _context.Players.OrderByDescending(p => p.Rank)
                                                   .ThenByDescending(p => p.CP)
                                                   .ThenBy(p => p.PlayerName);

                PlayerList = orderedtable.ToList();

                return Page();
            }

            using var reader = new StreamReader(csvFile.OpenReadStream());
            
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!line.Contains("Name,Rank,Watchtower lvl,CP,Last CP,Difference,Obelisk lvl,Time Zone"))
                {
                    var values = line.Split(',');

                    string playername = values[0];
                    string cp = string.Concat(values[3].Where(c => !char.IsWhiteSpace(c)));
                    string oldcp = string.Concat(values[4].Where(c => !char.IsWhiteSpace(c)));
                    string range = string.Concat(values[5].Where(c => !char.IsWhiteSpace(c)));
                    Rank rank = Player.GetRank(values[1]);
                    string wt = values[2];

                    if (!string.IsNullOrEmpty(playername))
                    {
                        // Beispiel: Name, CP, Rank, WatchtowerLevel
                        var player = new Player
                        {
                            PlayerName = playername,
                            CP = int.Parse(cp),
                            Rank = rank,
                            WatchtowerLevel = wt,
                        };

                        var cpentry = new CPEntry
                        {
                            Player = player,
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            PlayerID = player.ID,
                            Value = player.CP,
                        };

                        player.CP_List.Add(cpentry);

                        playerlist.Add(player);
                    }
                }
            }

            foreach (var player in playerlist)
            {
                if (!DbTools.ComparePlayer(player, _context.Players.ToList()))
                { 
                    _context.Players.Add(player);
                } 
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}