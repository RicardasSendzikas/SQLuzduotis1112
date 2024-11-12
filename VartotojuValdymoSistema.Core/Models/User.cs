namespace VartotojuValdymoSistema.Core.Models
{
    public enum UserRole
    {
        Admin,
        StandardUser
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; } // Pridėta roll savybė
    }
}