using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class OverviewModel : PageModel
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();
        public void OnGet()
        {
            PlayerList.Add(new Player()
            {
                PlayerName = "Wuwu13",
                CP = 135500000,
                Rank = Rank.R5,
                WatchtowerLevel = 30,
                Events = new(),
                Violationlist = new(),
            });
        }
    }
}
