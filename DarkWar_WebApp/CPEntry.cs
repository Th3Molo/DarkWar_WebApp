namespace DarkWar_WebApp
{
    public class CPEntry
    {
        public int ID { get; set; } // PK
        public int PlayerID { get; set; } // FK

        public DateOnly Date { get; set; }
        public long Value { get; set; }

        public Player Player { get; set; } // Navigation Property
    }
}
