using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.AuthDTOs
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public int? DzematId { get; set; } 
        public Roles Position { get; set; }
    }
}
