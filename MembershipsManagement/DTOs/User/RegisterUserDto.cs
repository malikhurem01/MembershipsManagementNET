using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.User
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ImeOca { get; set; } = string.Empty;
        public int? DzematId { get; set; }
        public Pozicija Pozicija { get; set; }
    }
}
