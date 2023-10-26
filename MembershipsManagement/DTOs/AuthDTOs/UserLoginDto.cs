using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.AuthDTOs
{
    public class UserLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? DzematId { get; set; } = string.Empty;
    }
}
