using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class GetMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public Status Status { get; set; }
        public double Debt { get; set; }
        public string? Note { get; set; } = string.Empty;
        public int EvNumber { get; set; }
        public List<GetFamilyMemberDto> FamilyMembers { get; set; } = new List<GetFamilyMemberDto>();
        public int DzematId { get; set; }
        public GetDzematDto? Dzemat { get; set; }
        public bool Active { get; set; }
    }
}
