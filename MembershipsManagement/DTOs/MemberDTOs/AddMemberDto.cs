using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class AddMemberDto
    {
        public int EvNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Debt { get; set; }
        public Status Status { get; set; }        
        public List<AddMemberFamilyDto>? FamilyMembers { get; set; } = new List<AddMemberFamilyDto>();
        public int DzematId { get; set; }
        public bool Active { get; set; }
    }
}
