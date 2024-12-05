using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class AddMemberFamilyListDto
    {
        public string MemberId { get; set; }
        public List<AddMemberFamilyDto> FamilyList { get; set; } = new List<AddMemberFamilyDto>();
    }
}
