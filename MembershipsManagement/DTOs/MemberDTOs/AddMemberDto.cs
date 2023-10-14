using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class AddMemberDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public Status Status { get; set; }
        public int MembershipFee { get; set; }
        public int EvNumber { get; set; }
        public List<AddMemberFamilyListDto>? FamilyMembers { get; set; } = new List<AddMemberFamilyListDto>();
        public string? DzematId { get; set; }
    }
}
