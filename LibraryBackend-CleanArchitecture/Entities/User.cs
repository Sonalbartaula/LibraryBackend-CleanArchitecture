using System.Runtime.CompilerServices;

namespace LibraryBackend_CleanArchitecture.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String Username { get; set; } = string.Empty;
        public String PasswordHash { get; set; } = string.Empty;
        public String Roles { get; set; } = string.Empty;
        public String RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }

    }
}