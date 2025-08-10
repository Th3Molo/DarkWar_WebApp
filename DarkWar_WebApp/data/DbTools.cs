using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DarkWar_WebApp.data
{
    public class DbTools
    {
        public static List<CPEntry> LoadCpEntry(int playerId)
        {
            var loadedCpEntry = new List<CPEntry>();

            using (var connection = new SqliteConnection("Data Source=game.db"))
            {
                connection.Open();

                using (var command = new SqliteCommand("SELECT date, value FROM CP_List WHERE player_id = @pid", connection))
                {
                    command.Parameters.AddWithValue("@pid", playerId);

                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var date = DateOnly.Parse(reader.GetString(0));
                        var value = reader.GetInt64(1);

                        loadedCpEntry.Add(new CPEntry
                        {
                            PlayerID = playerId,
                            Date = date,
                            Value = value
                        });
                    }
                }
            }

            return loadedCpEntry;
        }

        public static void AddCpEntry(int playerId, List<CPEntry> cplist)
        {
            using (var connection = new SqliteConnection("Data Source=game.db"))
            {
                connection.Open();

                // Tabelle erstellen (falls noch nicht vorhanden)
                using (var command = new SqliteCommand(@"CREATE TABLE IF NOT EXISTS CP_List (
                                                        player_id INTEGER NOT NULL,
                                                        date TEXT NOT NULL,
                                                        value INTEGER NOT NULL,
                                                        PRIMARY KEY (player_id, date))", connection))
                                                        {
                                                            command.ExecuteNonQuery();
                                                        }

                // Einfügen/aktualisieren
                foreach (var item in cplist)
                {
                    using (var command = new SqliteCommand("INSERT OR REPLACE INTO CP_List (player_id, date, value) VALUES (@pid, @date, @value)", connection))
                    {
                        command.Parameters.AddWithValue("@pid", playerId);
                        command.Parameters.AddWithValue("@date", item.Date.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@value", item.Value);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
