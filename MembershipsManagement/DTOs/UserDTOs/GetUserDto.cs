using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.User
{
    public class GetUserDto
    {
        public bool HasVerified { get; set; }
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? PlaceOfResidence { get; set; } = string.Empty;
        public string? PhoneNumber { get; set;} = string.Empty;
        public int? DzematId { get; set; }
        public GetDzematDto? Dzemat { get; set; }
        public int? MedzlisId { get; set; }
        public GetMedzlisDto? Medzlis { get; set; }
        public Roles Position { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}