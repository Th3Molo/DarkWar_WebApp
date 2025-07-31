namespace DarkWar_WebApp.data
{
    public class DbTools
    {
        public static void AddPlayer(AppDbContext context, Player player)
        {
            context.Add(player);
        }

        public static void UpdatePlayer(AppDbContext context, Player oldplayer, Player updatedPlayer)
        {
            oldplayer.PlayerName = updatedPlayer.PlayerName;
            oldplayer.CP = updatedPlayer.CP;
            oldplayer.Rank = updatedPlayer.Rank;
            oldplayer.WatchtowerLevel = updatedPlayer.WatchtowerLevel;
            oldplayer.Events = updatedPlayer.Events;

            context.Update(oldplayer);
        }
    }
}
