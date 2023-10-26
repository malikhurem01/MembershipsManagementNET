using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class GetMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int EvNumber { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; } = new List<FamilyMember>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public string DzematId { get; set; } = string.Empty;
    }
}
