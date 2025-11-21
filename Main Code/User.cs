namespace Contract_MC_System
{
    public class User
    {
        public int Id { get; set; }  // Primary Key
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Lecturer"; // Lecturer, Coordinator, Manager
    }
}
