using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.User
{
    public class GetUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public GetDzematDto? Dzemat { get; set; }
        public Roles Position { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}