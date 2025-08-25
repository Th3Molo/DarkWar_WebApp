namespace DarkWar_WebApp
{
    public class AppUser
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
    }
}
