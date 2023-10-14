using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberDTOs
{
    public class AddMemberFamilyListDto
    {
        public int MemberId { get; set; }
        public List<AddMemberFamilyDto> FamilyList { get; set; } = new List<AddMemberFamilyDto>();
    }
}
