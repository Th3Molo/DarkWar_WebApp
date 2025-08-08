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
        public int ID { get; set; } //PK
        public string PlayerName { get; set; } = string.Empty;
        public long CP { get; set; } = 0;
        public string WatchtowerLevel { get; set; } = string.Empty;
        public Rank Rank { get; set; } = Rank.R1;
        public string ViolationlistSerialized { get; set; } = string.Empty;
        public List<Events> Events { get; set; } = new List<Events>();
    }
}
