using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkWar_WebApp
{
    public enum Rank
    {
        R1 = 1,
        R2 = 2,
        R3 = 3,
        R4 = 4,
        R5 = 5,
    }

    public class Player
    {
        public int ID { get; set; } // PK
        public string PlayerName { get; set; } = string.Empty;
        public long CP { get; set; } = 0;

        // Navigation Property zu CP_List
        public List<CPEntry> CP_List { get; set; } = new List<CPEntry>();

        public string WatchtowerLevel { get; set; } = string.Empty;
        public Rank Rank { get; set; } = Rank.R1;
        public string ViolationlistSerialized { get; set; } = string.Empty;

        // Navigation Property zu Events
        public List<Events> Events { get; set; } = new List<Events>();

        public void AddCpToList(long cp, DateOnly date)
        {
            CPEntry cpentry = new()
            {
                Date = date,
                Player = this,
                PlayerID = this.ID,
                Value = cp,
            };

            CP_List.Add(cpentry);            
        }
    }
}
