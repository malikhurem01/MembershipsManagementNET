using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class GetMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int EvNumber { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();
        public string DzematId { get; set; } = string.Empty;
    }
}
