using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class AddMemberFamilyDto
    {
        public string FirstName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public FamilyStatus Status { get; set; }
    }
}
