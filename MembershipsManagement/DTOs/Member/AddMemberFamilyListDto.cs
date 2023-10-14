using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.Member
{
    public class AddMemberFamilyListDto
    {
        public int ClanId { get; set; }
        public List<AddMemberFamilyDto> FamilyList { get; set; }
    }
}
