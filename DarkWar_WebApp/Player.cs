using DarkWar_WebApp.data;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

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

            CompareCPEntry(cpentry);
        }

        public static Rank GetRank(string rank)
        {
            switch(rank)
            {
                case "1": return Rank.R1;
                case "2": return Rank.R2;
                case "3": return Rank.R3;
                case "4": return Rank.R4;
                case "5": return Rank.R5;
                default: return Rank.R1; 
            }
        }

        private void CompareCPEntry(CPEntry cpentry)
        {
            if (!CP_List.Any(entry => entry.Value == cpentry.Value))
            {
                DbTools.AddCpEntry(ID, CP_List);
            }            
        }
    }
}
